using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Message
{
    public class EventMassSendJobFinishMsg : EventBaseMsg
    {
        public override EventType Event
        {
            get { return EventType.MASSSENDJOBFINISH; }
        }

        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 群发的结构，为“send success”或“send fail”或“err(num)”。
        /// 但send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。
        /// err(num)是审核失败的具体原因，可能的情况如下：
        /// err(10001), //涉嫌广告 
        /// err(20001), //涉嫌政治 
        /// err(20004), //涉嫌社会 
        /// err(20002), //涉嫌色情 
        /// err(20006), //涉嫌违法犯罪 
        /// err(20008), //涉嫌欺诈 
        /// err(20013), //涉嫌版权 
        /// err(22000), //涉嫌互推(互相宣传) 
        /// err(21000), //涉嫌其他
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// group_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，
        /// 准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount { get; set; }
        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount { get; set; }

    }
}
