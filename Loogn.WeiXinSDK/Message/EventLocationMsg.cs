
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class EventLocationMsg : EventBaseMsg
    {
        public override string Event
        {
            get { return "location"; }
        }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }
    }
}
