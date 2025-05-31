using MeowToolsLib.Config.FileConfig.DataProcess;

namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置抽象类
/// </summary>
public abstract class FileConfigAbstract<T> where T : class, new()
{
    // 配置文件路径
    public abstract string FilePath { get; set; }
    
    // 配置文件类型
    public virtual FileConfigEnum.EnumFileType FileType { get; set; }
    
    // 配置文件格式
    public virtual FileConfigEnum.EnumFormatType FormatType { get; set; }
    
    // 存储结构体数据
    protected abstract T ConfigData { get; }
    
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        // 如果文件不存在，则创建文件
        if (File.Exists(FilePath)) return;
        File.Create(FilePath).Dispose();

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
            FileConfigEnum.EnumFileType.Json => Deserialize.Json<T>(configText),
            FileConfigEnum.EnumFileType.Xml => Deserialize.Xml<T>(configText),
            FileConfigEnum.EnumFileType.Yaml => Deserialize.Yaml<T>(configText),
            FileConfigEnum.EnumFileType.Toml => Deserialize.Toml<T>(configText),
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
        // 获取序列化数据
        var serializeData = GetSerializeData();

        // 根据文件类型进行数据序列化
        var configText = FileType switch
        {
            FileConfigEnum.EnumFileType.Json => Serialize.Json(serializeData),
            FileConfigEnum.EnumFileType.Xml => Serialize.Xml(serializeData),
            FileConfigEnum.EnumFileType.Yaml => Serialize.Yaml(serializeData),
            FileConfigEnum.EnumFileType.Toml => Serialize.Toml(serializeData),
            FileConfigEnum.EnumFileType.Binary => throw new NotSupportedException("Binary 未完成"),
            FileConfigEnum.EnumFileType.Auto => throw new NotSupportedException("Auto 未完成"),
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
            FileConfigEnum.EnumFileType.Json => Serialize.Json(ConfigData),
            FileConfigEnum.EnumFileType.Xml => Serialize.Xml(ConfigData),
            FileConfigEnum.EnumFileType.Yaml => Serialize.Yaml(ConfigData),
            FileConfigEnum.EnumFileType.Toml => Serialize.Toml(ConfigData),
            FileConfigEnum.EnumFileType.Binary => throw new NotSupportedException("Binary 未完成"),
            FileConfigEnum.EnumFileType.Auto => throw new NotSupportedException("Auto 未完成"),
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

    /// <summary>
    /// 获取序列化数据
    /// </summary>
    private Dictionary<string, object?> GetSerializeData()
    {
        // 获取当前类定义的所有公共实例属性
        var properties = typeof(T).GetProperties()
            .Where(p => p.DeclaringType == typeof(T) && !p.GetAccessors().Any(
                x => x is { IsVirtual: true, IsFinal: false })).ToArray();

        // 构建匿名对象，只包含当前类定义的属性
        return properties.ToDictionary(
            prop => prop.Name,
            prop => prop.GetValue(ConfigData)
        );
    }
}