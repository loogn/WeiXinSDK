using System.Collections.Generic;

namespace Loogn.WeiXinSDK
{
    public class GroupInfo
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分组名字，UTF8编码
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 分组内用户数量
        /// </summary>
        public int count { get; set; }

        public ReturnCode error { get; set; }
    }

    public class Groups : List<GroupInfo>
    {
        public ReturnCode error { get; set; }
    }

    public class GroupID
    {
        public int id { get; set; }
        public ReturnCode error { get; set; }
    }
}
