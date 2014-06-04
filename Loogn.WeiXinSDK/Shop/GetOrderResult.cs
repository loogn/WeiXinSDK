using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetOrderResult:ReturnCode
    {
        public Order order { get; set; }
    }
}
