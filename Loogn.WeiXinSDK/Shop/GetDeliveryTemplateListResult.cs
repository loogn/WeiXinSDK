using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetDeliveryTemplateListResult:ReturnCode
    {
        public List<DeliveryTemplate> templates_info { get; set; }
    }
}
