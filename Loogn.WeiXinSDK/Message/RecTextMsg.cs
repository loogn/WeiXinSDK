
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 接收的文本消息
    /// </summary>
    public class RecTextMsg : RecBaseMsg
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public override string MsgType
        {
            get { return "text"; }
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
