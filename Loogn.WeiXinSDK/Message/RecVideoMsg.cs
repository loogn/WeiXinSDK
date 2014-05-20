
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 接收的视频消息
    /// </summary>
    public class RecVideoMsg : RecBaseMsg
    {
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id
        /// </summary>
        public string ThumbMediaId { get; set; }

        public override string MsgType
        {
            get { return "video"; }
        }
    }
}
