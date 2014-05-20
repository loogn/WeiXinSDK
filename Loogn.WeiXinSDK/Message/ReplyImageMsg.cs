
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class ReplyImageMsg : ReplyBaseMsg
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }


        public override string MsgType
        {
            get { return "image"; }
        }

        protected override string GetXMLPart()
        {
            return "<Image><MediaId><![CDATA[" + MediaId + "]]></MediaId></Image>";
        }
    }
}
