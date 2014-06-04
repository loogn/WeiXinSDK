using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetShelfResult : ReturnCode
    {
        /// <summary>
        /// 货架名称
        /// </summary>
        public int shelf_id { get; set; }
        /// <summary>
        /// 货架信息
        /// </summary>
        public Loogn.WeiXinSDK.Shop.Shelf.Shelf_Data shelf_info { get; set; }
        /// <summary>
        /// 货架招牌图片URL(图片需要调用图片上传接口获得图片URL填写至此，
        /// 否则会显示出错,建议尺寸640*120，近控件1-4有banner，控件5没有banner)
        /// </summary>
        public string shelf_banner { get; set; }
        /// <summary>
        /// 货架名称
        /// </summary>
        public string shelf_name { get; set; }
    }
}
