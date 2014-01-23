
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class ReplyVoiceMsg : ReplyBaseMsg
    {
        public override MsgType MsgType
        {
            get { return Message.MsgType.voice; }
        }

        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        protected override string GetXMLPart()
        {
            return "<Voice><MediaId><![CDATA[" + MediaId + "]]></MediaId></Voice>";
        }

    }
}
