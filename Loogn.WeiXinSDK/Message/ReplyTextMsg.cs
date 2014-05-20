
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复文本消息
    /// </summary>
    public class ReplyTextMsg : ReplyBaseMsg
    {       
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }

        public override string MsgType
        {
            get { return "text"; }
        }
        protected override string GetXMLPart()
        {
            return "<Content><![CDATA[" + Content + "]]></Content>";
        }
    }

}
