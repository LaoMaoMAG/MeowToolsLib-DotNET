namespace MeowToolsLib.Config.FileConfig;

public static partial class FileConfigEnum
{
    /// <summary>
    /// 配置文件格式化类型
    /// </summary>
    public enum EnumFormatType
    {
        /// <summary>
        /// 默认模式
        /// </summary>
        Default,

        /// <summary>
        /// 大驼峰命名法（PascalCase）
        /// </summary>
        PascalCase,

        /// <summary>
        /// 小驼峰命名法（camelCase）
        /// </summary>
        CamelCase,

        /// <summary>
        /// 下划线分隔命名法（snake_case）
        /// </summary>
        SnakeCase,

        /// <summary>
        /// 常量命名法（UPPER_CASE）
        /// </summary>
        Constant
    }   
}