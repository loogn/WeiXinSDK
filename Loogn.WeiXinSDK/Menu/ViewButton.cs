
namespace Loogn.WeiXinSDK.Menu
{
    public class ViewButton:SingleButton
    {
        public override MenuType type
        {
            get { return MenuType.view; }
        }
        /// <summary>
        /// view类型必须.网页链接，用户点击菜单可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }
    }
}
