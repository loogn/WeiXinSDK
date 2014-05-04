using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class FilterMPNewsMess : FilterMess
    {
        public Media_IdObj mpnews { get; set; }

        public override string msgtype
        {
            get { return "mpnews"; }
        }
    }

    /// <summary>
    /// 文本
    /// </summary>
    public class FilterTextMess : FilterMess
    {
        public Media_IdObj text { get; set; }
        public override string msgtype
        {
            get { return "text"; }
        }
    }

    /// <summary>
    /// 语音
    /// </summary>
    public class FilterVoiceMess : FilterMess
    {
        public Media_IdObj voice { get; set; }
        public override string msgtype
        {
            get { return "voice"; }
        }
    }

    /// <summary>
    /// 图片
    /// </summary>
    public class FilterImageMess : FilterMess
    {
        public Media_IdObj image { get; set; }
        public override string msgtype
        {
            get { return "image"; }
        }
    }

    /// <summary>
    /// 视频
    /// </summary>
    public class FilterMPVideoMess : FilterMess
    {
        public Media_IdObj video { get; set; }
        public override string msgtype
        {
            get { return "mpvideo"; }
        }
    }


     
}
