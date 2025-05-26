namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 文件配置单例
/// </summary>
public class FileConfigSingleton<T>: FileConfigBase<T> where T : class, new()
{
    // 单例
    public static T Instance { get; } = new();
}