namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置基础类
/// 继承使用
/// </summary>
public class FileConfigBase<T> : FileConfigAbstract<T> where T : class, new()
{
    public override string FilePath { get; set; }

    protected override T ConfigData { get; }

    public FileConfigBase()
    {
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
        ConfigData = this as T;
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
        
        Init();
    }
}