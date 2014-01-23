
namespace Loogn.WeiXinSDK.Menu
{
    public abstract class SingleButton:BaseButton
    {
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 菜单的响应动作类型，目前有click、view两种类型
        /// </summary>
        public abstract MenuType type { get;}
    }
}
