using System;

namespace Loogn.WeiXinSDK.Message
{
    public abstract class RecBaseMsg : RecEventBaseMsg
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public Int64 MsgId { get; set; }
    }
}