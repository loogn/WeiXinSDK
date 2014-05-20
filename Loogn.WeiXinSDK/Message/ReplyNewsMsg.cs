using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 回复图文消息
    /// </summary>
    public class ReplyNewsMsg : ReplyBaseMsg
    {
        public override string MsgType
        {
            get { return "news"; }
        }
        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// </summary>
        public List<News> Articles { get; set; }

        public class News
        {
            /// <summary>
            /// 图文消息标题(可选)
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 图文消息描述(可选)
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200(可选)
            /// </summary>
            public string PicUrl { get; set; }
            /// <summary>
            /// 点击图文消息跳转链接(可选)
            /// </summary>
            public string Url { get; set; }
        }

        protected override string GetXMLPart()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", Articles.Count);
            sb.AppendFormat("<Articles>");
            foreach (var news in Articles)
            {
                sb.Append("<item>");
                sb.AppendFormat("<Title><![CDATA[{0}]]></Title>",news.Title);
                sb.AppendFormat("<Description><![CDATA[{0}]]></Description>", news.Description);
                sb.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", news.PicUrl);
                sb.AppendFormat("<Url><![CDATA[{0}]]></Url>", news.Url);
                sb.Append("</item>");
            }
            sb.AppendFormat("</Articles>");
            return sb.ToString();
        }
    }
}
