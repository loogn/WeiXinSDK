using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    /// <summary>
    /// 用基础接口的media_id得到群发可用的mdeia_id
    /// </summary>
    public class UploadVideoInfo
    {
        /// <summary>
        /// 通过基础支持中的上传下载多媒体文件来得到
        /// </summary>
        public string media_id { get; set; }

        public string title { get; set; }

        public string description { get; set; }
    }
}
