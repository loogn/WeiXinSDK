using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Message
{
    public class EventViewMsg:EventBaseMsg
    {
        public override string Event
        {
            get { return "view"; }
        }

        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }
}
