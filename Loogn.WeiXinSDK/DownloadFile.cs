using System.IO;

namespace Loogn.WeiXinSDK
{
    public class DownloadFile
    {
        public Stream Stream { get; set; }
        /// <summary>
        ///  image/jpeg等
        /// </summary>
        public string ContentType { get; set; }
        public ReturnCode error { get; set; }
    }
}
