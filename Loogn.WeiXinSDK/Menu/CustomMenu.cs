using System.Collections.Generic;

namespace Loogn.WeiXinSDK.Menu
{
    public class CustomMenu
    {
        public List<BaseButton> button = new List<BaseButton>();

        public virtual string GetJSON()
        {
            return Util.ToJson(this);
        }
    }
}
