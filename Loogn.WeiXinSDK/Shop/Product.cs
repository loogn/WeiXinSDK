using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string product_id { get; set; }
        
        /// <summary>
        /// 基本属性
        /// </summary>
        public Product_Base product_base { get; set; }

        /// <summary>
        /// sku信息列表(可多个),每个sku信息串即为一个确定的商品，比如白色的37码的鞋子
        /// </summary>
        public List<ProductSKU> sku_list { get; set; }

        /// <summary>
        /// 商品其他信息
        /// </summary>
        public ProductAttrext attrext { get; set; }

        /// <summary>
        /// 运费信息--必选
        /// </summary>
        public ProductDelivery_Info delivery_info { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte status { get; set; }


        /// <summary>
        /// product_base
        /// </summary>
        public class Product_Base
        {
            /// <summary>
            /// 商品分类id,商品分类列表请通过WeiXinShop.GetSubCateList获取--必选
            /// </summary>
            public List<string> category_id { get; set; }

            /// <summary>
            /// 商品属性列表，属性列表通过XeiXinShop.GetPropertyListByCate获取
            /// </summary>
            public List<Property_Info> property { get; set; }

            /// <summary>
            /// 商品名称--必选
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 商品sku定义,sku列表请通过WeiXinShop.GetSKUListBySubCate获取
            /// </summary>
            public List<SKU_Info> sku_info { get; set; }

            /// <summary>
            /// 商品原价(单位为分)
            /// </summary>
            public int ori_price { get; set; }

            /// <summary>
            /// 商品主力(图片需要调用图片上传接口获得图片URL填写一至此，否则可能会显示出错。图片分辨率推荐尺寸为640*600)--必选
            /// </summary>
            public string main_img { get; set; }
            /// <summary>
            /// 商品图片列表(图片需要调用图片上传接口获得图片URL填写一至此，否则可能会显示出错。图片分辨率推荐尺寸为640*600)--必选
            /// </summary>
            public List<string> img { get; set; }

            /// <summary>
            /// 商品详情列表，显示在客户端的商品详情页内文字描述--必选
            /// </summary>
            public List<Detail> detail { get; set; }

            /// <summary>
            /// 用户商品限购数量
            /// </summary>
            public int buy_limit { get; set; }


            public class SKU_Info
            {
                /// <summary>
                /// sku属性(sku列表中id,支持自定义sku，格式为"$xxx"，xxx即为显示在客户端中的字符串)
                /// </summary>
                public string id { get; set; }

                /// <summary>
                /// sku值(sku列表中的vid，如需自定义sku，格式为"$xxx"，xxx即为显示在客户端中的字符串)
                /// </summary>
                public List<string> vid { get; set; }
            }

            public class Property_Info
            {
                /// <summary>
                /// 属性id
                /// </summary>
                public string id { get; set; }
                /// <summary>
                /// 属性值id
                /// </summary>
                public string vid { get; set; }
            }

            /// <summary>
            /// 详细信息
            /// </summary>
            public abstract class Detail
            {
            }

            /// <summary>
            /// 文字描述
            /// </summary>
            public class TextDetail : Detail
            {
                public string text { get; set; }
            }

            /// <summary>
            /// 图片信息
            /// </summary>
            public class ImgDetail : Detail
            {
                public string img { get; set; }
            }


        }

        public class ProductSKU
        {
            /// <summary>
            /// sku信息,参考sku_table的定义；格式为："id1:vid1;id2:vid2"
            /// 规格：id_info的组合个数必须与sku_table个数一致
            /// （若商品无sku信息,即商品为统一规格,则此处赋值为空字符串即可）
            /// </summary>
            public string sku_id { get; set; }

            /// <summary>
            /// sku微信价（单位分）
            /// </summary>
            public int price { get; set; }
            /// <summary>
            /// sku iconurl（图片需调用图片上传接口获得图片url）
            /// </summary>
            public string icon_url { get; set; }
            /// <summary>
            /// 商家商品编码
            /// </summary>
            public string product_code { get; set; }
            /// <summary>
            /// sku原价（单位分）
            /// </summary>
            public int ori_price { get; set; }

            /// <summary>
            /// sku库存
            /// </summary>
            public int quantity { get; set; }

        }

        public class ProductAttrext
        {
            /// <summary>
            /// 商品所在地地址
            /// </summary>
            public Location location { get; set; }
            /// <summary>
            /// 是否包邮(0否，1是)，如果包邮delivery_info字段可省略
            /// </summary>
            public byte isPostFree { get; set; }
            /// <summary>
            /// 是否提供发票(0否，1是)
            /// </summary>
            public byte isHasReceipt { get; set; }
            /// <summary>
            /// 是否保修(0否，1是)
            /// </summary>
            public byte isUnderGuaranty { get; set; }
            /// <summary>
            /// 是否支持退换货(0否，1是)
            /// </summary>
            public byte isSupportReplace { get; set; }

            public class Location
            {
                /// <summary>
                /// 国家
                /// </summary>
                public string country { get; set; }
                /// <summary>
                /// 省份
                /// </summary>
                public string province { get; set; }
                /// <summary>
                /// 城市
                /// </summary>
                public string city { get; set; }
                /// <summary>
                /// 地址
                /// </summary>
                public string address { get; set; }
            }
        }

        public class ProductDelivery_Info
        {
            /// <summary>
            /// 运费类型（0使用express字段的默认模板，
            /// 1使用template_id代表的邮费模板
            /// </summary>
            public byte delivery_type { get; set; }
            /// <summary>
            /// 邮费模板id
            /// </summary>
            public byte template_id { get; set; }
            /// <summary>
            /// 快递信息
            /// </summary>
            public List<Express> express { get; set; }

            public class Express
            {
                /// <summary>
                /// 快递id(平邮：10000027，快递：10000028，EMS:10000029)
                /// </summary>
                public int id { get; set; }
                /// <summary>
                /// 运费（单位分）
                /// </summary>
                public int price { get; set; }
            }

        }

    }
}
