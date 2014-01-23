
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 取消关注事件
    /// </summary>
    public class EventUnattendMsg : EventBaseMsg
    {
        public override EventType Event
        {
            get { return EventType.unsubscribe; }
        }
    }
}
