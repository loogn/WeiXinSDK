using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK
{
    class WebCredential
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期秒数
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string scope { get; set; }

        public ReturnCode error { get; set; }

        static Dictionary<string, WebCredential> creds = new Dictionary<string, WebCredential>();
        static string TokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";


        //internal static WebCredential GetCredential(string appId, string appSecret,string code)
        //{
        //    WebCredential cred = null;
        //    if (creds.TryGetValue(appId, out cred))
        //    {
        //        if (cred.add_time.AddSeconds(cred.expires_in - 30) < DateTime.Now)
        //        {
        //            creds.Remove(appId);
        //            cred = null;
        //        }
        //        else
        //        {
        //            return cred;
        //        }
        //    }
        //    var json = Util.HttpGet2(string.Format(TokenUrl, appId, appSecret));
        //    if (json.IndexOf("errcode") >= 0)
        //    {
        //        cred = new ClientCredential();
        //        cred.error = Util.JsonTo<ReturnCode>(json);
        //    }
        //    else
        //    {
        //        cred = Util.JsonTo<ClientCredential>(json);
        //        creds[appId] = cred;
        //    }
        //    return cred;
        //}
    }
}
