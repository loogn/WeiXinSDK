using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetDeliveryTemplateResult:ReturnCode
    {
        public DeliveryTemplate template_info { get; set; }
    }
}
