using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Mass
{
    public abstract class FilterMess : BaseMess
    {
        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public FilterObj filter { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class FilterObj
    {
        /// <summary>
        /// 群发到的分组的group_id
        /// </summary>
        public string group_id { get; set; }
    }
}
