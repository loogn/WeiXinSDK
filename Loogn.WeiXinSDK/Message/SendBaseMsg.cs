
namespace Loogn.WeiXinSDK.Message
{
    /// <summary>
    /// 主动发消息的基类。
    /// 当用户主动发消息给公众号的时候（包括发送信息、点击自定义菜单click事件、订阅事件、扫描二维码事件、支付成功事件、用户维权），
    /// 微信将会把消息数据推送给开发者，
    /// 开发者在一段时间内（目前修改为48小时）可以调用客服消息接口，
    /// 通过POST一个JSON数据包来发送消息给普通用户，在48小时内不限制发送次数。
    /// 此接口主要用于客服等有人工消息处理环节的功能，
    /// 方便开发者为用户提供更加优质的服务。
    /// </summary>
    public abstract class SendBaseMsg
    {
        public string touser { get; set; }
        public abstract string msgtype { get; }

        public virtual string GetJSON()
        {
            return Util.ToJson(this);
        }
    }
}
