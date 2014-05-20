
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class ReplyMusicMsg : ReplyBaseMsg
    {
        public override string MsgType
        {
            get { return "music"; }
        }

        /// <summary>
        /// 音乐标题(可选)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 音乐描述(可选)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 音乐链接(可选)
        /// </summary>
        public string MusicURL { get; set; }

        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐(可选)
        /// </summary>
        public string HQMusicUrl { get; set; }

        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }

        protected override string GetXMLPart()
        {
            return "<Music><Title><![CDATA[" + Title + "]]></Title><Description><![CDATA[" + Description + "]]></Description><MusicUrl><![CDATA[" + MusicURL + "]]></MusicUrl><HQMusicUrl><![CDATA[" + HQMusicUrl + "]]></HQMusicUrl><ThumbMediaId><![CDATA[" + ThumbMediaId + "]]></ThumbMediaId></Music>";
        }
    }
}
