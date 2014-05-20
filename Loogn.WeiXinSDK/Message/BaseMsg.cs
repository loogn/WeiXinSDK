using System;

namespace Loogn.WeiXinSDK.Message
{
    
    public abstract class BaseMsg
    {
        /// <summary>
        /// 接收方帐号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public Int64 CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract string MsgType { get; }

        public override string ToString()
        {
            return Util.ToJson(this);
        }

    }
}
