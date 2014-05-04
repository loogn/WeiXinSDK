using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    public abstract class ToUserMess : BaseMess
    {
        public List<string> touser { get; set; }
    }
}
