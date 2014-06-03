using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class UpdateGroupProduct
    {
        public long group_id { get; set; }
        public List<ProductAction> product { get; set; }

        public class ProductAction
        {
            public string product_id { get; set; }
            /// <summary>
            /// 修改操作（0删除，1增加）
            /// </summary>
            public byte mod_action { get; set; }
        }
    }
}
