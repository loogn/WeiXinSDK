
namespace Loogn.WeiXinSDK.Message
{
    public class ReplyEmptyMsg:ReplyBaseMsg
    {
        public override string MsgType
        {
            get { return "text"; }
        }
        public override string GetXML()
        {
            return string.Empty;
        }

        public static ReplyEmptyMsg Instance = new ReplyEmptyMsg();
    }
}
