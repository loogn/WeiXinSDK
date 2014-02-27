using System.Collections.Generic;

namespace Loogn.WeiXinSDK.Menu
{
    public class CustomMenu
    {
        public List<BaseButton> button = new List<BaseButton>();

        public void AddMulitButton(MultiButton multiBtn)
        {
            button.Add(multiBtn);
        }

        public void AddClickButton(ClickButton clickBtn)
        {
            button.Add(clickBtn);
        }

        public void AddViewButton(ViewButton viewBtn)
        {
            button.Add(viewBtn);
        }


        public virtual string GetJSON()
        {
            return Util.ToJson(this);
        }
    }
}
