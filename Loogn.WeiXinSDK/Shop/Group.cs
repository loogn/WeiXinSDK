using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class Group
    {
        public long group_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 商品id集合
        /// </summary>
        public List<string> product_list { get; set; }

    }
}
