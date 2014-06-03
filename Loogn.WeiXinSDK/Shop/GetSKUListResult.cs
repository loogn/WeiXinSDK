using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetSKUListResult:ReturnCode
    {
        public List<SKU_Table> sku_table { get; set; }

        public class SKU_Table : ID_Name
        {
            public List<ID_Name> value_list { get; set; }
        }
    }
}
