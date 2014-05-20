
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 发送图片消息
    /// </summary>
    public class SendImageMsg:SendBaseMsg
    {
        public override string msgtype
        {
            get { return "image"; }
        }

        public Image image { get; set; }

        public class Image
        {
            /// <summary>
            /// 发送的图片的媒体ID
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
