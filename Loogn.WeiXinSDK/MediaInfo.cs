
namespace Loogn.WeiXinSDK
{
  
    /// <summary>
    ///  媒体文件
    /// </summary>
    public class MediaInfo
    {
        /// <summary>
        /// 媒体文件类型,image,voice,video,thumb,news
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public long created_at { get; set; }

        public ReturnCode error { get; set; }
    }
}
