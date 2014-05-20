
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 接收的语音消息
    /// </summary>
    public class RecVoiceMsg:RecBaseMsg
    {
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format{get;set;}


        //Recognition	 语音识别结果，UTF8编码
        /// <summary>
        /// 语音识别结果，UTF8编码(开通语音识别功能后才会有)
        /// </summary>
        public string Recognition { get; set; }

        public override string MsgType
        {
            get { return "voice"; }
        }
    }
}
