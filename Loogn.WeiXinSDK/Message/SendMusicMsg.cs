
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 发送音乐消息
    /// </summary>
    public class SendMusicMsg : SendBaseMsg
    {
        public override MsgType msgtype
        {
            get { return MsgType.music; }
        }

        public class Music
        {
            /// <summary>
            /// 音乐标题(可选)
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 音乐描述(可选)
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// 音乐链接
            /// </summary>
            public string musicurl { get; set; }
            /// <summary>
            /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
            /// </summary>
            public string hqmusicurl { get; set; }
            /// <summary>
            /// 缩略图的媒体ID
            /// </summary>
            public string thumb_media_id { get; set; }
        }
    }
}
