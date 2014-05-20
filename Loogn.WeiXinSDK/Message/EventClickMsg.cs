
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 自定义菜单事件
    /// </summary>
    public class EventClickMsg:EventBaseMsg
    {
        public override string Event
        {
            get { return "click"; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
