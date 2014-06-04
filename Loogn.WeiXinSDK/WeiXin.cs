using System;
using System.Collections.Generic;
using System.IO;
using Loogn.WeiXinSDK.Menu;
using Loogn.WeiXinSDK.Message;
using Loogn.WeiXinSDK.Mass;

namespace Loogn.WeiXinSDK
{
    /// <summary>
    /// 消息事件处理委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public delegate TResult MyFunc<T1, TResult>(T1 t);
    public delegate void SetAccessTokenHandler(ClientCredential credential);
    public delegate string GetAccessTokenHandler();

    /// <summary>
    /// 微信接口API
    /// </summary>
    public class WeiXin
    {
        internal static string AppID, AppSecret;
        static object lockObj = new object();
        /// <summary>
        /// 设置全局appId和appSecret,一般只用在应用程序启动时调用一次即可
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void ConfigGlobalCredential(string appId, string appSecret)
        {
            AppID = appId;
            AppSecret = appSecret;
        }
        static SetAccessTokenHandler m_setHandler;
        static GetAccessTokenHandler m_getHandler;
        /// <summary>
        /// 设置AccessToken缓存方法
        /// </summary>
        /// <param name="setHandler"></param>
        /// <param name="getHandler"></param>
        public static void ConfigAccessTokenCache(SetAccessTokenHandler setHandler, GetAccessTokenHandler getHandler)
        {
            m_setHandler = setHandler;
            m_getHandler = getHandler;
        }

        /// <summary>
        /// 得到AccessToken
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string GetAccessToken(string appId, string appSecret)
        {
            if (m_setHandler == null || m_getHandler == null)
            {
                throw new ArgumentNullException("setHandler,getHandler", "请先调用ConfigAccessTokenCache");
            }
            lock (lockObj)
            {
                var at = m_getHandler();
                if (string.IsNullOrEmpty(at))
                {
                    var credential = ClientCredential.GetCredential(appId, appSecret);
                    m_setHandler(credential);
                    at = credential.access_token;
                }
                return at;
            }
        }

        public static string GetAccessToken()
        {
            CheckGlobalCredential();
            return GetAccessToken(AppID, AppSecret);
        }

        /// <summary>
        /// 检验signature
        /// </summary>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="token">由AppId和AppSecret得到的凭据</param>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token)
        {
            if (string.IsNullOrEmpty(signature)) return false;
            List<string> tmpList = new List<string>(3);
            tmpList.Add(token);
            tmpList.Add(timestamp);
            tmpList.Add(nonce);
            tmpList.Sort();
            var tmpStr = string.Join("", tmpList.ToArray());
            string strResult = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            return signature.Equals(strResult, StringComparison.InvariantCultureIgnoreCase);
        }

        #region 消息

        /// <summary>
        /// 处理用户消息和事件
        /// </summary>
        /// <returns></returns>
        public static ReplyBaseMsg ReplyMsg()
        {
            Stream inputStream = System.Web.HttpContext.Current.Request.InputStream;
            long pos = inputStream.Position;
            inputStream.Position = 0;
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, buffer.Length);
            inputStream.Position = pos;
            string xml = System.Text.Encoding.UTF8.GetString(buffer);
            var dict = Util.GetDictFromXml(xml);


            string key = string.Empty;
            ReplyBaseMsg replyMsg = ReplyEmptyMsg.Instance;
            if (dict.ContainsKey("Event"))
            {
                #region 接收事件消息
                var evt = dict["Event"].ToLower();
                key = "event_";
                switch (evt)
                {
                    case "click":
                        {
                            var msg = new EventClickMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.Click, EventKey = dict["EventKey"] };
                            replyMsg = GetReply<EventClickMsg>(key+MyEventType.Click.ToString() , msg);
                            break;
                        }
                    case "view":
                        {
                            var msg = new EventViewMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.View, EventKey = dict["EventKey"] };
                            replyMsg = GetReply<EventViewMsg>(key + MyEventType.View.ToString(), msg);
                            break;
                        }
                    case "location":
                        {
                            var msg = new EventLocationMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.Location, Latitude = double.Parse(dict["Latitude"]), Longitude = double.Parse(dict["Longitude"]), Precision = double.Parse(dict["Precision"]) };
                            replyMsg = GetReply<EventLocationMsg>(key+MyEventType.Location.ToString(), msg);
                            break;
                        }
                    case "scan":
                        {
                            var msg = new EventFansScanMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.FansScan, EventKey = dict["EventKey"], Ticket = dict["Ticket"] };
                            replyMsg = GetReply<EventFansScanMsg>(key+MyEventType.FansScan.ToString(), msg);
                            break;
                        }
                    case "unsubscribe":
                        {
                            var msg = new EventUnattendMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.Unattend };
                            replyMsg = GetReply<EventUnattendMsg>(key+MyEventType.Unattend.ToString(), msg);
                            break;
                        }
                    case "subscribe":
                        {
                            if (dict.ContainsKey("Ticket"))
                            {
                                var msg = new EventUserScanMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.UserScan, Ticket = dict["Ticket"], EventKey = dict["EventKey"] };
                                replyMsg = GetReply<EventUserScanMsg>(key+MyEventType.UserScan.ToString(), msg);
                            }
                            else
                            {
                                var msg = new EventAttendMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MyEventType = MyEventType.Attend };
                                replyMsg = GetReply<EventAttendMsg>(key + MyEventType.Attend.ToString(), msg);
                            }
                            break;
                        }
                    case "masssendjobfinish":
                        {
                            var msg = new EventMassSendJobFinishMsg
                            {
                                CreateTime = Int64.Parse(dict["CreateTime"]),
                                FromUserName = dict["FromUserName"],
                                ToUserName = dict["ToUserName"],
                                MyEventType = MyEventType.MASSSENDJOBFINISH,
                                ErrorCount = int.Parse(dict["ErrorCount"]),
                                FilterCount = int.Parse(dict["FilterCount"]),
                                MsgID = int.Parse(dict["MsgID"]),
                                SentCount = int.Parse(dict["SentCount"]),
                                TotalCount = int.Parse(dict["TotalCount"]),
                                Status = dict["Status"]
                            };

                            replyMsg = GetReply<EventMassSendJobFinishMsg>(key + MyEventType.MASSSENDJOBFINISH.ToString(), msg);
                            break;
                        }
                    case "merchant_order":
                        {
                            var msg = new EventMerchantOrderMsg
                            {
                                CreateTime = Int64.Parse(dict["CreateTime"]),
                                FromUserName = dict["FromUserName"],
                                ToUserName = dict["ToUserName"],
                                MyEventType = MyEventType.MerchantOrder,
                                OrderID = dict["OrderID"],
                                OrderStatus = int.Parse(dict["OrderStatus"]),
                                ProductId = dict["ProductId"],
                                SkuInfo = dict["SkuInfo"]
                            };
                            replyMsg = GetReply<EventMerchantOrderMsg>(key + MyEventType.MerchantOrder.ToString(), msg);
                            break;
                        }
                }
                #endregion
            }
            else if (dict.ContainsKey("MsgId"))
            {
                #region 接收普通消息
                var msgType =  dict["MsgType"];
                key = msgType;
                switch (msgType)
                {
                    case "text":
                        {
                            var msg = new RecTextMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), Content = dict["Content"] };
                            replyMsg = GetReply<RecTextMsg>(key, msg);
                            break;
                        }
                    case "image":
                        {

                            var msg = new RecImageMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), PicUrl = dict["PicUrl"], MediaId = dict["MediaId"] };
                            replyMsg = GetReply<RecImageMsg>(key, msg);
                            break;
                        }
                    case "voice":
                        {
                            string recognition;
                            dict.TryGetValue("Recognition", out recognition);
                            var msg = new RecVoiceMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), Format = dict["Format"], MediaId = dict["MediaId"], Recognition = recognition };
                            replyMsg = GetReply<RecVoiceMsg>(key, msg);
                            break;
                        }
                    case "video":
                        {
                            var msg = new RecVideoMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), ThumbMediaId = dict["ThumbMediaId"], MediaId = dict["MediaId"] };
                            replyMsg = GetReply<RecVideoMsg>(key, msg);
                            break;
                        }
                    case "location":
                        {
                            var msg = new RecLocationMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), Label = dict["Label"], Location_X = double.Parse(dict["Location_X"]), Location_Y = double.Parse(dict["Location_Y"]), Scale = int.Parse(dict["Scale"]) };
                            replyMsg = GetReply<RecLocationMsg>(key, msg);
                            break;
                        }
                    case "link":
                        {
                            var msg = new RecLinkMsg { CreateTime = Int64.Parse(dict["CreateTime"]), FromUserName = dict["FromUserName"], ToUserName = dict["ToUserName"], MsgId = long.Parse(dict["MsgId"]), Description = dict["Description"], Title = dict["Title"], Url = dict["Url"] };
                            replyMsg = GetReply<RecLinkMsg>(key, msg);
                            break;
                        }
                }
                #endregion
            }
            return replyMsg;
        }

        static Dictionary<string, object> m_msgHandlers = new Dictionary<string, object>();
        
        /// <summary>
        /// 注册消息处理程序
        /// </summary>
        /// <typeparam name="TMsg"></typeparam>
        /// <param name="handler"></param>
        public static void RegisterMsgHandler<TMsg>(MyFunc<TMsg, ReplyBaseMsg> handler) where TMsg : RecBaseMsg
        {
            var type = typeof(TMsg);
            var key = string.Empty;
            if (type == typeof(RecTextMsg))
            {
                key = "text";
            }
            else if (type == typeof(RecImageMsg))
            {
                key = "image";
            }
            else if (type == typeof(RecLinkMsg))
            {
                key = "link";
            }
            else if (type == typeof(RecLocationMsg))
            {
                key = "location";
            }
            else if (type == typeof(RecVideoMsg))
            {
                key = "video";
            }
            else if (type == typeof(RecVoiceMsg))
            {
                key = "voice";
            }
            else
            {
                return;
            }
            m_msgHandlers[key.ToLower()] = handler;
        }
        /// <summary>
        /// 注册事件处理程序
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="handler"></param>
        public static void RegisterEventHandler<TEvent>(MyFunc<TEvent, ReplyBaseMsg> handler) where TEvent : EventBaseMsg
        {
            var type = typeof(TEvent);
            var key = "event_";
            if (type == typeof(EventClickMsg))
            {
                key += MyEventType.Click.ToString();
            }
            else if (type == typeof(EventFansScanMsg))
            {
                key += MyEventType.FansScan.ToString();
            }
            else if (type == typeof(EventAttendMsg))
            {
                key += MyEventType.Attend.ToString();
            }
            else if (type == typeof(EventLocationMsg))
            {
                key += MyEventType.Location.ToString();
            }
            else if (type == typeof(EventUnattendMsg))
            {
                key += MyEventType.Unattend.ToString();
            }
            else if (type == typeof(EventUserScanMsg))
            {
                key += MyEventType.UserScan.ToString();
            }
            else if (type == typeof(EventMassSendJobFinishMsg))
            {
                key += MyEventType.MASSSENDJOBFINISH.ToString();
            }
            else if (type == typeof(EventViewMsg))
            {
                key += MyEventType.View.ToString();
            }
            else if (type == typeof(EventMerchantOrderMsg))
            {
                key += MyEventType.MerchantOrder.ToString();
            }
            else
            {
                return;
            }
            m_msgHandlers[key.ToLower()] = handler;
        }

        static ReplyBaseMsg GetReply<TMsg>(string key, TMsg msg) where TMsg : RecEventBaseMsg
        {
            key = key.ToLower();
            if (m_msgHandlers.ContainsKey(key))
            {
                var handler = m_msgHandlers[key] as MyFunc<TMsg, ReplyBaseMsg>;
                var replyMsg = handler(msg);
                if (replyMsg.CreateTime == 0) replyMsg.CreateTime = DateTime.Now.Ticks;
                if (string.IsNullOrEmpty(replyMsg.FromUserName)) replyMsg.FromUserName = msg.ToUserName;
                if (string.IsNullOrEmpty(replyMsg.ToUserName)) replyMsg.ToUserName = msg.FromUserName;
                return replyMsg;
            }
            else
            {
                return ReplyEmptyMsg.Instance;
            }
        }

        /// <summary>
        /// 主动给用户发消息（用户）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns>errcode=0为成功</returns>
        public static ReturnCode SendMsg(SendBaseMsg msg, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = msg.GetJSON();

            var retJson = Util.HttpPost2(url, json);
            return Util.JsonTo<ReturnCode>(retJson);
        }

        /// <summary>
        /// 主动给用户发消息（用户）,用会SetGlobalCredential方法设置的appId和appSecret来得到access_token
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ReturnCode SendMsg(SendBaseMsg msg)
        {
            CheckGlobalCredential();
            return SendMsg(msg, AppID, AppSecret);
        }


        #endregion

        #region 群发
        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public SendReturnCode SendMessByGroup(FilterMess mess, string appId, string appSecret)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.ToJson(mess);
            var retJson = Util.HttpPost2(url, json);
            return Util.JsonTo<SendReturnCode>(retJson);
        }

        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public SendReturnCode SendMessByGroup(FilterMess mess)
        {
            CheckGlobalCredential();
            return SendMessByGroup(mess, AppID, AppSecret);
        }

        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public SendReturnCode SendMessByUsers(ToUserMess mess, string appId, string appSecret)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.ToJson(mess);
            var retJson = Util.HttpPost2(url, json);
            return Util.JsonTo<SendReturnCode>(retJson);
        }

        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public SendReturnCode SendMessByUsers(ToUserMess mess)
        {
            CheckGlobalCredential();
            return SendMessByUsers(mess, AppID, AppSecret);
        }

        /// <summary>
        /// 删除群发.
        /// 请注意，只有已经发送成功的消息才能删除删除消息只是将消息的图文详情页失效，已经收到的用户，还是能在其本地看到消息卡片。 另外，删除群发消息只能删除图文消息和视频消息，其他类型的消息一经发送，无法删除。
        /// </summary>
        /// <param name="msgid"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public ReturnCode DeleteMess(int msgid, string appId, string appSecret)
        {
            var url = "https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = "{\"msgid\":" + msgid.ToString() + "}";
            var retJson = Util.HttpPost2(url, json);
            return Util.JsonTo<ReturnCode>(retJson);
        }


        #endregion

        #region 自定义菜单

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static ReturnCode CreateMenu(CustomMenu menu, string appId, string appSecret)
        {
            var json = menu.GetJSON();
            return CreateMenu(json, appId, appSecret);
        }

        public static ReturnCode CreateMenu(CustomMenu menu)
        {
            CheckGlobalCredential();
            return CreateMenu(menu, AppID, AppSecret);
        }

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="menuJSON"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static ReturnCode CreateMenu(string menuJSON, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var retJson = Util.HttpPost2(url, menuJSON);
            return Util.JsonTo<ReturnCode>(retJson);
        }

        public static ReturnCode CreateMenu(string menuJSON)
        {
            CheckGlobalCredential();
            return CreateMenu(menuJSON, AppID, AppSecret);
        }

        /// <summary>
        /// 直接返回自定义菜单json字符串，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string GetMenu(string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.HttpGet2(url);
            return json;
        }

        public static string GetMenu()
        {
            CheckGlobalCredential();
            return GetMenu(AppID, AppSecret);
        }

        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static ReturnCode DeleteMenu(string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.HttpGet2(url);
            return Util.JsonTo<ReturnCode>(json);
        }

        public static ReturnCode DeleteMenu()
        {
            CheckGlobalCredential();
            return DeleteMenu(AppID, AppSecret);
        }

        #endregion

        #region 二维码

        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="isTemp"></param>
        /// <param name="scene_id"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static QRCodeTicket CreateQRCode(bool isTemp, int scene_id, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var action_name = isTemp ? "QR_SCENE" : "QR_LIMIT_SCENE";
            string data;
            if (isTemp)
            {
                data = "{\"expire_seconds\": 1800, \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\":" + scene_id + "}}}";
            }
            else
            {
                data = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + scene_id + "}}}";
            }

            var json = Util.HttpPost2(url, data);
            if (json.IndexOf("ticket") > 0)
            {
                return Util.JsonTo<QRCodeTicket>(json);
            }
            else
            {
                QRCodeTicket tk = new QRCodeTicket();
                tk.error = Util.JsonTo<ReturnCode>(json);
                return tk;
            }
        }

        /// <summary>
        /// 创建QRCode
        /// </summary>
        /// <param name="isTemp"></param>
        /// <param name="scene_id"></param>
        /// <returns></returns>
        public static QRCodeTicket CreateQRCode(bool isTemp, int scene_id)
        {
            CheckGlobalCredential();
            return CreateQRCode(isTemp, scene_id, AppID, AppSecret);
        }

        /// <summary>
        /// 得到QR图片地址
        /// </summary>
        /// <param name="qrcodeTicket"></param>
        /// <returns></returns>
        public static string GetQRUrl(string qrcodeTicket)
        {
            return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + System.Web.HttpUtility.HtmlEncode(qrcodeTicket);
        }

        #endregion

        #region 获取关注者列表

        /// <summary>
        /// 获取关注者列表
        /// </summary>
        /// <param name="next_openid"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static Followers GetFollowers(string next_openid, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            if (!string.IsNullOrEmpty(next_openid))
            {
                url = url + "&next_openid=" + next_openid;
            }
            var json = Util.HttpGet2(url);
            if (json.IndexOf("errcode") > 0)
            {
                var fs = new Followers();
                fs.error = Util.JsonTo<ReturnCode>(json);
                return fs;
            }
            else
            {
                return Util.JsonTo<Followers>(json);
            }
        }

        public static Followers GetFollowers(string next_openid)
        {
            CheckGlobalCredential();
            return GetFollowers(next_openid, AppID, AppSecret);
        }

        /// <summary>
        /// 获取所有关注者列表
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static Followers GetAllFollowers(string appId, string appSecret)
        {
            Followers allFollower = new Followers();
            allFollower.data = new Followers.Data();
            allFollower.data.openid = new List<string>();

            string next_openid = string.Empty;
            do
            {
                var f = GetFollowers(next_openid, appId, appSecret);
                if (f.error != null)
                {
                    allFollower.error = f.error;
                    break;
                }
                else
                {
                    if (f.count > 0)
                    {
                        foreach (var opid in f.data.openid)
                        {
                            allFollower.data.openid.Add(opid);
                        }
                    }
                    next_openid = f.next_openid;
                }
            } while (!string.IsNullOrEmpty(next_openid));

            allFollower.count = allFollower.total;
            return allFollower;
        }

        public static Followers GetAllFollowers()
        {
            CheckGlobalCredential();
            return GetAllFollowers(AppID, AppSecret);
        }

        #endregion

        #region 用户信息
        /// <summary>
        /// 得到用户基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="lang"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string openid, LangType lang, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token + "&openid=" + openid + "&lang=" + lang.ToString();

            var json = Util.HttpGet2(url);

            if (json.IndexOf("errcode") > 0)
            {
                var ui = new UserInfo();
                ui.error = Util.JsonTo<ReturnCode>(json);
                return ui;
            }
            else
            {
                return Util.JsonTo<UserInfo>(json);
            }
        }

        public static UserInfo GetUserInfo(string openid, LangType lang)
        {
            CheckGlobalCredential();
            return GetUserInfo(openid, lang, AppID, AppSecret);
        }

        #endregion

        #region 分组

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static GroupInfo CreateGroup(string name, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var post = "{\"group\":{\"name\":\"" + name + "\"}}";
            var json = Util.HttpPost2(url, post);
            if (json.IndexOf("errcode") > 0)
            {
                var gi = new GroupInfo();
                gi.error = Util.JsonTo<ReturnCode>(json);
                return gi;
            }
            else
            {
                var dict = Util.JsonTo<Dictionary<string, Dictionary<string, object>>>(json);
                var gi = new GroupInfo();
                var gpdict = dict["group"];
                gi.id = Convert.ToInt32(gpdict["id"]);
                gi.name = gpdict["name"].ToString();
                return gi;
            }
        }

        public static GroupInfo CreateGroup(string name)
        {
            CheckGlobalCredential();
            return CreateGroup(name, AppID, AppSecret);
        }

        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static Groups GetGroups(string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            string json = Util.HttpGet2(url);
            if (json.IndexOf("errcode") > 0)
            {
                var gs = new Groups();
                gs.error = Util.JsonTo<ReturnCode>(json);
                return gs;
            }
            else
            {
                var dict = Util.JsonTo<Dictionary<string, List<Dictionary<string, object>>>>(json);
                var gs = new Groups();
                var gilist = dict["groups"];
                foreach (var gidict in gilist)
                {
                    var gi = new GroupInfo();
                    gi.name = gidict["name"].ToString();
                    gi.id = Convert.ToInt32(gidict["id"]);
                    gi.count = Convert.ToInt32(gidict["count"]);
                    gs.Add(gi);
                }
                return gs;
            }
        }

        public static Groups GetGroups()
        {
            CheckGlobalCredential();
            return GetGroups(AppID, AppSecret);
        }

        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static GroupID GetUserGroup(string openid, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var post = "{\"openid\":\"" + openid + "\"}";
            var json = Util.HttpPost2(url, post);
            if (json.IndexOf("errcode") > 0)
            {
                var gid = new GroupID();
                gid.error = Util.JsonTo<ReturnCode>(json);
                return gid;
            }
            else
            {
                var dict = Util.JsonTo<Dictionary<string, int>>(json);
                var gid = new GroupID();
                gid.id = dict["groupid"];
                return gid;
            }
        }

        public static GroupID GetUserGroup(string openid)
        {
            CheckGlobalCredential();
            return GetUserGroup(openid, AppID, AppSecret);
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static ReturnCode UpdateGroup(int id, string name, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var post = "{\"group\":{\"id\":" + id + ",\"name\":\"" + name + "\"}}";
            var json = Util.HttpPost2(url, post);
            return Util.JsonTo<ReturnCode>(json);
        }

        public static ReturnCode UpdateGroup(int id, string name)
        {
            CheckGlobalCredential();
            return UpdateGroup(id, name, AppID, AppSecret);
        }
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static ReturnCode MoveGroup(string openid, int groupid, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var post = "{\"openid\":\"" + openid + "\",\"to_groupid\":" + groupid + "}";
            var json = Util.HttpPost2(url, post);
            return Util.JsonTo<ReturnCode>(json);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static ReturnCode MoveGroup(string openid, int groupid)
        {
            CheckGlobalCredential();
            return MoveGroup(openid, groupid, AppID, AppSecret);
        }

        #endregion

        #region 多媒体文件
        /// <summary>
        /// 上传多媒体文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"> 媒体文件类型,image,voice,video,thumb,news</param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static MediaInfo UploadMedia(string file, string type, string appId, string appSecret)
        {
            string url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token + "&type=" + type.ToString();
            var json = Util.HttpUpload(url, file);
            if (json.IndexOf("errcode") > 0)
            {
                var mi = new MediaInfo();
                mi.error = Util.JsonTo<ReturnCode>(json);
                return mi;
            }
            else
            {
                return Util.JsonTo<MediaInfo>(json);
            }
        }

        public static MediaInfo UploadMedia(string file, string type)
        {
            CheckGlobalCredential();
            return UploadMedia(file, type, AppID, AppSecret);
        }

        public static MediaInfo UploadVideoForMess(UploadVideoInfo videoInfo,string appId, string appSecret)
        {
            var url = "https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token=";
            var access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.HttpPost2(url, Util.ToJson(videoInfo));
            if (json.IndexOf("errcode") > 0)
            {
                var mi = new MediaInfo();
                mi.error = Util.JsonTo<ReturnCode>(json);
                return mi;
            }
            else
            {
                return Util.JsonTo<MediaInfo>(json);
            }
        }

        public static MediaInfo UploadVideoForMess(UploadVideoInfo videoInfo)
        {
            CheckGlobalCredential();
            return UploadVideoForMess(videoInfo, AppID, AppSecret);
        }

        public static MediaInfo UploadNews(News news, string appId, string appSecret)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token;
            var json = Util.HttpPost2(url, Util.ToJson(news));
            if (json.IndexOf("errcode") > 0)
            {
                var mi = new MediaInfo();
                mi.error = Util.JsonTo<ReturnCode>(json);
                return mi;
            }
            else
            {
                return Util.JsonTo<MediaInfo>(json);
            }
        }

        /// <summary>
        /// 上传图文消息素材,用于群发
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public static MediaInfo UploadNews(News news)
        {
            CheckGlobalCredential();
            return UploadNews(news, AppID, AppSecret);
        }

        /// <summary>
        /// 下载多媒体文件
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static DownloadFile DownloadMedia(string media_id, string appId, string appSecret)
        {
            string url = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=";
            string access_token = GetAccessToken(appId, appSecret);
            url = url + access_token + "&media_id=" + media_id;
            var tup = Util.HttpGet(url);
            var dm = new DownloadFile();
            dm.ContentType = tup.Item2;
            
            if (tup.Item1 == null)
            {
                dm.error = Util.JsonTo<ReturnCode>(tup.Item3);
            }
            else
            {
                dm.Stream = tup.Item1;
            }
            return dm;
        }

        public static DownloadFile DownloadMedia(string media_id)
        {
            CheckGlobalCredential();
            return DownloadMedia(media_id, AppID, AppSecret);
        }

        #endregion

        #region 网页授权获取用户基本信息
        /// <summary>
        /// 得到获取code的Url
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="redirect">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <returns></returns>
        public static string BuildWebCodeUrl(string appid,string redirect,string scope,string state="")
        {
            
            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect", appid, redirect, scope, state);
        }
        /// <summary>
        /// 得到获取code的Url
        /// </summary>
        /// <param name="redirect">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <returns></returns>
        public static string BuildWebCodeUrl(string redirect, string scope, string state = "")
        {
            CheckGlobalCredential();
            return BuildWebCodeUrl(AppID, redirect, scope, state);
        }
        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WebCredential GetWebAccessToken(string appId, string appSecret, string code)
        {
            return WebCredential.GetCredential(appId, appSecret, code);
        }

        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WebCredential GetWebAccessToken(string code)
        {
            CheckGlobalCredential();
            return GetWebAccessToken(AppID, AppSecret, code);
        }

        /// <summary>
        /// 得到网页授权用户信息
        /// </summary>
        /// <param name="access_token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openid">用户的唯一标识</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static WebUserInfo GetWebUserInfo(string access_token,string openid,  LangType lang)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}", access_token, openid, lang.ToString());
            
            var json = Util.HttpGet2(url);

            if (json.IndexOf("errcode") > 0)
            {
                var ui = new WebUserInfo();
                ui.error = Util.JsonTo<ReturnCode>(json);
                return ui;
            }
            else
            {
                return Util.JsonTo<WebUserInfo>(json);
            }
        }
        #endregion


        internal static void CheckGlobalCredential()
        {
            if (string.IsNullOrEmpty(AppID) || string.IsNullOrEmpty(AppSecret))
            {
                throw new ArgumentNullException("全局AppID,AppSecret", "请先调用SetGlobalCredential");
            }
        }
    }
}
