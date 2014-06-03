using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class DeliveryTemplate
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 邮费模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 支付方式（0买家承担运费，1卖家承担运费）
        /// </summary>
        public byte Assumer { get; set; }

        /// <summary>
        /// 计费单位（0按件计费，1按重量计费，2按体积计费，目前只支持按件计费，默认为0）
        /// </summary>
        public byte Valuation { get; set; }

        /// <summary>
        /// 具体运费计算
        /// </summary>
        public List<TopFreeInfo> TopFee { get; set; }

        public class TopFreeInfo
        {
            /// <summary>
            /// 快递id(平邮：10000027，快递：10000028，EMS:10000029)
            /// </summary>
            public int Type { get; set; }
            /// <summary>
            /// 默认邮费计算方法
            /// </summary>
            public NormalInfo Normal { get; set; }
            /// <summary>
            /// 指定地区邮费计算方法
            /// </summary>
            public List<CustomInfo> Custom { get; set; }

            public class NormalInfo
            {
                /// <summary>
                /// 起始计费数量（比如计费单位是按件，填2代表起始计费为2件）
                /// </summary>
                public int StartStandards { get; set; }
                /// <summary>
                /// 起始计费金额（单位为分）
                /// </summary>
                public int StartFees { get; set; }
                /// <summary>
                /// 递增计费数量
                /// </summary>
                public int AddStandards { get; set; }
                /// <summary>
                /// 递增计费金额（单位为分）
                /// </summary>
                public int AddFees { get; set; }
            }

            public class CustomInfo:NormalInfo
            {
                /// <summary>
                /// 指定国家
                /// </summary>
                public string DestCountry { get; set; }
                /// <summary>
                /// 指定省份
                /// </summary>
                public string DestProvince { get; set; }
                /// <summary>
                /// 指定城市
                /// </summary>
                public string DestCity { get; set; }
            }
        }
    }

}
