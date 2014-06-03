using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetProductListResult:ReturnCode
    {
        public List<Product> products_info { get; set; }
    }
}
