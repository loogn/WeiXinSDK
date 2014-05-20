
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复视频消息
    /// </summary>
    public class ReplyVideoMsg : ReplyBaseMsg
    {
        public override string MsgType
        {
            get { return "video"; }
        }

        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }

        protected override string GetXMLPart()
        {
            return "<Video><MediaId><![CDATA[" + MediaId + "]]></MediaId><Title><![CDATA[" + Title + "]]></Title><Description><![CDATA[" + Description + "]]></Description></Video>";
        }
    }
}
