using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetOrderListResult:ReturnCode
    {
        public List<Order> order_list { get; set; }
    }
}
