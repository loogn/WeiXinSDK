
namespace Loogn.WeiXinSDK
{
    /// <summary>
    /// 媒体文件类型
    /// </summary>
    public enum MediaType
    { 
        /// <summary>
        /// 图片  128K，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音 256K，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频  1MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// 缩略图（主要用于视频与音乐格式的缩略图） 64KB，支持JPG格式
        /// </summary>
        thumb

    }
    /// <summary>
    ///  媒体文件
    /// </summary>
    public class MediaInfo
    {
        /// <summary>
        /// 媒体文件类型
        /// </summary>
        public MediaType type { get; set; }
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
