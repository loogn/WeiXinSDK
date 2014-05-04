using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    public class News
    {
        /// <summary>
        /// 图文消息，一个图文消息支持1到10条图文
        /// </summary>
        public List<Article> articles { get; set; }

    }
}
