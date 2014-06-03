using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    /// <summary>
    /// 添加商品返回值
    /// </summary>
    public class AddProductResult:ReturnCode
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string product_id { get; set; }
    }
}
