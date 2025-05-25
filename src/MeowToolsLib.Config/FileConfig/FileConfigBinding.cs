namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置数据绑定
/// </summary>
public partial class FileConfigBinding<T> where T : class, new()
{
    // 配置文件路径
    public string FilePath { get; set; }

    // 配置文件类型
    public EnumFileType FileType { get; set; }

    // 配置文件格式
    public EnumFormatType FormatType { get; set; }

    // 存储结构体数据
    public T ConfigData { get; set; }

    /// <summary>
    /// 构造方法
    /// </summary>
    public FileConfigBinding(string filePath, T configData, EnumFileType fileType = EnumFileType.Json,
        EnumFormatType formatType = EnumFormatType.Default)
    {
        FilePath = filePath;
        ConfigData = configData;
        FileType = fileType;
        FormatType = formatType;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        // 如果文件不存在，则创建文件
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath).Dispose();
        }

        // 保存配置文件
        Save();
    }

    /// <summary>
    /// 加载
    /// </summary>
    public void Load()
    {
        // 读取文件数据倒加载缓存
        var configText = File.ReadAllText(FilePath);

        // 根据文件类型进行数据反序列化
        var loadedData = FileType switch
        {
            EnumFileType.Json => Deserialize.Json<T>(configText),
            EnumFileType.Xml => Deserialize.Xml<T>(configText),
            EnumFileType.Yaml => Deserialize.Yaml<T>(configText),
            EnumFileType.Toml => Deserialize.Toml<T>(configText),
            _ => throw new ArgumentOutOfRangeException()
        };

        // 检查数据是否反序列化成功
        if (loadedData == null)
        {
            throw new InvalidOperationException("配置数据反序列化失败，无法加载。");
        }

        // 拷贝属性
        CopyProperties(loadedData, ConfigData);
    }

    /// <summary>
    /// 保存
    /// </summary>
    public void Save()
    {
        // 根据文件类型进行数据序列化
        var configText = FileType switch
        {
            EnumFileType.Json => Serialize.Json(ConfigData),
            EnumFileType.Xml => Serialize.Xml(ConfigData),
            EnumFileType.Yaml => Serialize.Yaml(ConfigData),
            EnumFileType.Toml => Serialize.Toml(ConfigData),
            _ => throw new ArgumentOutOfRangeException()
        };

        //  检查数据是否序列化成功
        if (string.IsNullOrEmpty(configText))
        {
            throw new InvalidOperationException("配置数据序列化失败，无法保存。");
        }

        // 保存文件
        File.WriteAllText(FilePath, configText);
    }

    /// <summary>
    /// 获取配置文件文本
    /// </summary>
    public string? Dump()
    {
        return FileType switch
        {
            EnumFileType.Json => Serialize.Json(ConfigData),
            EnumFileType.Xml => Serialize.Xml(ConfigData),
            EnumFileType.Yaml => Serialize.Yaml(ConfigData),
            EnumFileType.Toml => Serialize.Toml(ConfigData),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// 拷贝属性
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <typeparam name="TObj"></typeparam>
    private void CopyProperties<TObj>(TObj source, TObj target)
    {
        foreach (var prop in typeof(TObj).GetProperties())
        {
            if (prop.CanWrite && prop.GetAccessors().Any(x => x.IsPublic))
            {
                prop.SetValue(target, prop.GetValue(source));
            }
        }
    }
}