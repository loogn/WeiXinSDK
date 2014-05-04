using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    public abstract class BaseMess
    {
        /// <summary>
        /// 群发的消息类型，图文消息为mpnews，文本消息为text，语音为voice，音乐为music，图片为image，视频为video
        /// </summary>
        public abstract string msgtype { get; }
    }

    public class Media_IdObj
    {
        public string media_id { get; set; }
    }

}
