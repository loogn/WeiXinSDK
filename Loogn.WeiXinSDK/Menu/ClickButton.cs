
namespace Loogn.WeiXinSDK.Menu
{
    public class ClickButton : SingleButton
    {
        public override string type
        {
            get { return "click"; }
        }
        /// <summary>
        /// click类型必须.菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }
    }
}
