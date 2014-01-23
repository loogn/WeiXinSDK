
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 发送视频消息
    /// </summary>
    public class SendVideoMsg:SendBaseMsg
    {
        public override MsgType msgtype
        {
            get { return MsgType.video; }
        }

        public Video video { get; set; }

        public class Video
        {
            /// <summary>
            /// 发送的视频的媒体ID
            /// </summary>
            public string media_id { get; set; }

            /// <summary>
            /// 视频消息的标题(可选)
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 视频消息的描述(可选)
            /// </summary>
            public string description { get; set; }
        }
    }
}
