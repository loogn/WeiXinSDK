using System;
using System.Collections.Generic;

namespace Loogn.WeiXinSDK
{
    /// <summary>
    /// 凭据
    /// </summary>
    [Serializable]
    public class ClientCredential
    {
        public string access_token { get; set; }
        /// <summary>
        /// 过期秒数
        /// </summary>
        public int expires_in { get; set; }

        public ReturnCode error { get; set; }

        static string TokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        internal static ClientCredential GetCredential(string appId, string appSecret)
        {
            ClientCredential cred = null;
            var json = Util.HttpGet2(string.Format(TokenUrl, appId, appSecret));
            if (json.IndexOf("errcode") >= 0)
            {
                cred = new ClientCredential();
                cred.error = Util.JsonTo<ReturnCode>(json);
            }
            else
            {
                cred = Util.JsonTo<ClientCredential>(json);
            }
            return cred;
        }
    }
}
