using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    /// <summary>
    /// 群发返回值 
    /// </summary>
    public class SendReturnCode:ReturnCode
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息为news
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public int msg_id { get; set; }
    }
}
