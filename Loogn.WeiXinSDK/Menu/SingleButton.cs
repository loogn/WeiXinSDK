
namespace Loogn.WeiXinSDK.Menu
{
    
    public abstract class SingleButton:BaseButton
    {
        /// <summary>
        /// 菜单的响应动作类型，目前有click、view两种类型
        /// </summary>
        public abstract string type { get;}
    }
}
