namespace MeowToolsLib.Config.FileConfig;


public static partial class FileConfigEnum
{
    /// <summary>
    /// 配置文件类型枚举
    /// </summary>
    public enum EnumFileType
    {
        /// <summary>
        /// Json
        /// </summary>
        Json,
        
        /// <summary>
        /// Xml
        /// </summary>
        Xml,
        
        /// <summary>
        /// Yaml
        /// </summary>
        Yaml,
        
        /// <summary>
        /// Toml
        /// </summary>
        Toml,
        
        /// <summary>
        /// 二进制
        /// </summary>
        Binary,
        
        /// <summary>
        /// 自动
        /// </summary>
        Auto
    }
}