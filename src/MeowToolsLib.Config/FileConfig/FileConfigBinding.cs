namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置数据绑定
/// </summary>
public class FileConfigBinding<T> : FileConfigAbstract<T> where T : class, new()
{
    public override string FilePath { get; set; }
    
    protected override T ConfigData { get; } 
    
    /// <summary>
    /// 构造方法
    /// </summary>
    public FileConfigBinding(string filePath, T configData,
        FileConfigEnum.EnumFileType fileType = FileConfigEnum.EnumFileType.Json,
        FileConfigEnum.EnumFormatType formatType = FileConfigEnum.EnumFormatType.Default)
    {
        FilePath = filePath;
        ConfigData = configData;
        FileType = fileType;
        FormatType = formatType;
    }
}