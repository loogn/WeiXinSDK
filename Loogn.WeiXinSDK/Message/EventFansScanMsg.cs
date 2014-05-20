
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 用户已关注时,扫描带参数二维码事件
    /// </summary>
    public class EventFansScanMsg:EventBaseMsg
    {
        public override string Event
        {
            get { return "scan"; }
        }
        
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值scene_id
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

    }
}
