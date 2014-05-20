
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class RecLinkMsg:RecBaseMsg
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        public override string MsgType
        {
            get { return "link"; }
        }
    }
}