
namespace Loogn.WeiXinSDK.Message
{
    public class EventAttendMsg : EventBaseMsg
    {
        public override EventType Event
        {
            get { return EventType.subscribe; }
        }
    }
}
