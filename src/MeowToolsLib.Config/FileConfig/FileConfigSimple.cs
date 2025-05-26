namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置简单模式
/// </summary>
public class FileConfigSimple : FileConfigAbstract<Dictionary<string, object>>
{
    // 配置文件路径
    public override string FilePath { get; set; }
    
    protected override Dictionary<string, object> ConfigData { get; } 
    
    /// <summary>
    /// 构造方法
    /// </summary>
    public FileConfigSimple(string filePath, FileConfigEnum.EnumFileType fileType = FileConfigEnum.EnumFileType.Json,
        FileConfigEnum.EnumFormatType formatType = FileConfigEnum.EnumFormatType.Default)
    {
        
    }
    
    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(string key, object value)
    {
        
    }
    
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? Get<T>(string key)
    {
        return default;
    }
    
    /// <summary>
    /// 判断键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsExist(string key)
    {
        return false;
    }
    
    /// <summary>
    /// 移除键
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Remove(string key)
    {
        return false;
    }
}