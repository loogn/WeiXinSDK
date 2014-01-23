
namespace Loogn.WeiXinSDK.Message
{
    public abstract class ReplyBaseMsg : BaseMsg
    {
        public virtual string GetXML()
        {
            return "<xml><ToUserName><![CDATA[" + ToUserName + "]]></ToUserName><FromUserName><![CDATA[" + FromUserName + "]]></FromUserName><CreateTime>" + CreateTime + "</CreateTime><MsgType><![CDATA[" + MsgType.ToString() + "]]></MsgType>" + GetXMLPart() + "</xml>";
        }

        protected virtual string GetXMLPart()
        {
            return string.Empty;
        }
    }
}
