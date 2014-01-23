
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 订阅
        /// </summary>
        subscribe,
        /// <summary>
        /// 取消订阅
        /// </summary>
        unsubscribe,
        /// <summary>
        /// 已关注用户扫描带参数二维码
        /// </summary>
        scan,
        /// <summary>
        /// 上报地理位置
        /// </summary>
        LOCATION,
        /// <summary>
        /// 点击自定义菜单
        /// </summary>
        CLICK,
        /// <summary>
        /// 进入会话
        /// </summary>
        ENTER

    }
}
