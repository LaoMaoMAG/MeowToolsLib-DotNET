using System.Text.Json;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MeowToolsLib.Config.FileConfig.DataProcess;

/// <summary>
/// 配置反序列化
/// </summary>
internal static class Deserialize
{
    public static T? Json<T>(string text)
    {
        if (string.IsNullOrEmpty(text)) return default;
        return JsonSerializer.Deserialize<T>(text);
    }
    
    public static T? Xml<T>(string text)
    {
        if (string.IsNullOrEmpty(text)) return default;
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(text);
        return (T?)serializer.Deserialize(reader);
    }
    
    public static T? Yaml<T>(string text)
    {
        if (string.IsNullOrEmpty(text)) return default;
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return deserializer.Deserialize<T>(text);
    }
    
    public static T? Toml<T>(string text) where T : class, new()
    {
        return string.IsNullOrEmpty(text) ? null : Tomlyn.Toml.ToModel<T>(text);
    }
}