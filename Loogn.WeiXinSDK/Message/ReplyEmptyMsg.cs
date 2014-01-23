
namespace Loogn.WeiXinSDK.Message
{
    public class ReplyEmptyMsg:ReplyBaseMsg
    {
        public override MsgType MsgType
        {
            get { return Message.MsgType.text; }
        }
        public override string GetXML()
        {
            return string.Empty;
        }

        public static ReplyEmptyMsg Instance = new ReplyEmptyMsg();
    }
}
