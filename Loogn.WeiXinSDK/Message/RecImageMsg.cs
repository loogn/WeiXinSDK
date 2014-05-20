
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 接收的图片消息
    /// </summary>
    public class RecImageMsg:RecBaseMsg
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        public override string MsgType
        {
            get { return "image"; }
        }
    }
}
