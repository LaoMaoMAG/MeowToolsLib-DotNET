using System.Text.Json;
using System.Xml.Serialization;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MeowToolsLib.Config.FileConfig;

/// <summary>
/// 配置序列化
/// </summary>
internal static class Serialize
{
    public static string? Json<T>(T data)
    {
        if (data == null) return null;
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(data, options);
    }
    
    public static string? Xml<T>(T data)
    {
        if (data == null) return null;
        var serializer = new XmlSerializer(typeof(T));
        using var writer = new StringWriter();
        serializer.Serialize(writer, data);
        return writer.ToString();
    }
    
    public static string? Yaml<T>(T data)
    {
        if (data == null) return null;
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return serializer.Serialize(data);
    }
    
    public static string? Toml<T>(T data)
    {
        return data == null ? null : Tomlyn.Toml.FromModel(data);
    }
}