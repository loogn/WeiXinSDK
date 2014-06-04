using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    /// <summary>
    /// 订单详细
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单状态 2待发货，3已发货，5已完成，8维权中
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 订单总价格（单位分）
        /// </summary>
        public int order_total_price { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public long order_create_time { get; set; }
        /// <summary>
        /// 订单运费价格（单位分）
        /// </summary>
        public int order_express_price { get; set; }
        /// <summary>
        /// 买家微信openid
        /// </summary>
        public string buyer_openid { get; set; }
        /// <summary>
        /// 买家微信昵称
        /// </summary>
        public string buyer_nick { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receiver_name { get; set; }
        /// <summary>
        /// 收货地址省份
        /// </summary>
        public string receiver_province { get; set; }
        /// <summary>
        /// 收货地址城市
        /// </summary>
        public string receiver_city { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string receiver_address { get; set; }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string receiver_mobile { get; set; }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string receiver_phone { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 商品价格（单位分）
        /// </summary>
        public int product_price { get; set; }
        /// <summary>
        /// 商品sku
        /// </summary>
        public string product_sku { get; set; }
        /// <summary>
        /// 商品个数
        /// </summary>
        public int product_count { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string product_img { get; set; }
        /// <summary>
        /// 运单id
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 物流公司编码
        /// 邮政EMS：Fsearch_code,
        /// 申通快递：002shentong,
        /// 中通速递：066zhongtong,
        /// 圆通速递：056yuantong,
        /// 天天快递：042tiantian,
        /// 顺丰速递：003shunfeng,
        /// 韵达快递：059Yunda,
        /// 宅急送：064zhaijisong,
        /// 汇通快递：020huitong,
        /// 易迅快递：zj001yixun
        /// </summary>
        public string delivery_company { get; set; }
        /// <summary>
        /// 交易id
        /// </summary>
        public string trans_id { get; set; }

    }
}
