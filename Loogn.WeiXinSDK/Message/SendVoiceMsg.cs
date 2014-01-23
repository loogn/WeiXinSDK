
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 发送语音消息
    /// </summary>
    public class SendVoiceMsg:SendBaseMsg
    {
        public override MsgType msgtype
        {
            get { return MsgType.voice; }
        }

        public Voice voice { get; set; }

        public class Voice
        {
            /// <summary>
            /// 发送的语音的媒体ID
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
