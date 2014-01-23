using System.Collections.Generic;

namespace Loogn.WeiXinSDK.Menu
{
    public class MultiButton:BaseButton
    {
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        public List<SingleButton> sub_button = new List<SingleButton>();
    }
}
