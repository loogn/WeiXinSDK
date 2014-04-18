
using System;
namespace Loogn.WeiXinSDK
{
    /// <summary>
    /// 全局返回码请看
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E5%85%A8%E5%B1%80%E8%BF%94%E5%9B%9E%E7%A0%81%E8%AF%B4%E6%98%8E
    /// </summary>
    [Serializable]
    public class ReturnCode
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public override string ToString()
        {
            return "{ \"errcode\":" + errcode + ",\"errmsg\":\"" + errmsg + "\"}";
        }
    }
}
