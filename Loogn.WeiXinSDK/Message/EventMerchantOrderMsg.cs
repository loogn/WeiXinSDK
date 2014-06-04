using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Message
{
    public class EventMerchantOrderMsg : EventBaseMsg
    {
        public override string Event
        {
            get { return "merchant_order"; }
        }

        public string OrderID { get; set; }
        /// <summary>
        /// 2待发货，3已发货，5已完成，8维权中
        /// </summary>
        public int OrderStatus { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// 格式为："1001：10000012;1002:1000032"
        /// </summary>
        public string SkuInfo { get; set; }
    }
}
