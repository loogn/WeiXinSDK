using System.Collections.Generic;

namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 发送图文消息
    /// </summary>
    public class SendNewsMsg:SendBaseMsg
    {
        public override MsgType msgtype
        {
            get { return MsgType.news; }
        }

        public News news { get; set; }

        public class News
        {
            /// <summary>
            /// 图文消息条数限制在10条以内，注意，如果图文数超过10，则将会无响应。
            /// </summary>
            public List<Article> articles { get; set; }

            public class Article
            {
                /// <summary>
                /// 图文消息标题(可选)
                /// </summary>
                public string title { get; set; }

                /// <summary>
                /// 图文消息描述(可选)
                /// </summary>
                public string description { get; set; }

                /// <summary>
                /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80(可选)
                /// </summary>
                public string picurl { get; set; }
                /// <summary>
                /// 点击图文消息跳转链接(可选)
                /// </summary>
                public string url { get; set; }
            }

        }
    }
}
