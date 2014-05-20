
namespace Loogn.WeiXinSDK.Message
{
    public abstract class EventBaseMsg : RecEventBaseMsg
    {
        public abstract string Event { get; }

        public override string MsgType
        {
            get { return "Event"; }
        }

        public MyEventType MyEventType { get; set; }
    }
}
