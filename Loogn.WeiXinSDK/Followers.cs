using System.Collections.Generic;

namespace Loogn.WeiXinSDK
{
    public class Followers
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 拉取列表的后一个用户的OPENID
        /// </summary>
        public string next_openid { get; set; }

        public class Data
        {
            public List<string> openid { get; set; }
        }

        public ReturnCode error { get; set; }
    }
}
