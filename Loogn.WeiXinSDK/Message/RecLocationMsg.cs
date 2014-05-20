
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 接收的地理位置消息
    /// </summary>
    public class RecLocationMsg:RecBaseMsg
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public double Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }

        public override string MsgType
        {
            get { return "location"; }
        }
    }
}
