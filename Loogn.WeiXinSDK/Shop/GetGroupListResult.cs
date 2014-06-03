using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetGroupListResult:ReturnCode
    {
        public List<Group> groups_detail { get; set; }
    }
}
