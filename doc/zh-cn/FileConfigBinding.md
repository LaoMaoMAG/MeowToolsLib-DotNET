# `FileConfigBinding<T>` 使用文档

> **注意：此方法正在开发，随时会进行大幅度修改！**

`FileConfigBinding<T>` 是一个泛型类，用于绑定配置数据到文件，并支持多种配置文件格式（JSON、XML、YAML、TOML）。
它提供了一个封装后简单的调用方法，以及强声明的配置数据结构。
使用时需要定义一个数据结构，并使用 `FileConfigBinding<T>` 绑定到文件，对数据结构进行修改后调用对应的方法即可同步到文件。

## 命名空间
```csharp
namespace MeowToolsLib.Config.FileConfig;
```


## 泛型约束
- [T](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L14-L14) 必须是引用类型 (`class`)。
- [T](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L14-L14) 必须有无参数的构造函数 (`new()`)

## 枚举类型依赖
| 枚举 | 说明 |
|------|------|
| `EnumFileType` | 文件类型，支持 JSON、XML、YAML、TOML。 |
| [EnumFormatType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\EnmuFormatType.cs#L2-L5) | 格式类型，默认为 `Default`，在当前实现中未被实际使用。 |

## 主要属性

| 属性名称 | 类型 | 说明 |
|----------|------|------|
| [FilePath](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L8-L8) | `string` | 配置文件的路径。 |
| [FileType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L11-L11) | `EnumFileType` | 配置文件的类型，决定如何进行序列化/反序列化。 |
| [FormatType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L14-L14) | [EnumFormatType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\EnmuFormatType.cs#L2-L5) | 配置文件格式（如压缩、美化等），目前未被使用。 |
| [ConfigData](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L17-L17) | [T](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L14-L14) | 当前配置的数据对象，存储实际的配置信息。 |

## 构造方法

```csharp
public FileConfigBinding(string filePath, T configData, EnumFileType fileType = EnumFileType.Json, EnumFormatType formatType = EnumFormatType.Default)
```

**参数说明：**
- `filePath`: 配置文件路径。
- `configData`: 初始配置数据对象。
- `fileType`: 配置文件类型，默认为 EnumFileType.Json 详细见下方枚举类型。
- `formatType`: 配置文件格式，默认为 EnumFormatType.Default 详细见下方枚举类型（未完成，设置无效）。

## 公开方法

### [public void Init()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L34-L44)
初始化配置文件：
1. 如果文件不存在，则创建空文件。
2. 调用 [Save()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L77-L97) 方法将默认配置写入文件。

### [public void Load()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L49-L72)
从文件中加载并解析配置内容，根据 [FileType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L11-L11) 使用对应的反序列化方式还原数据，并更新 [ConfigData](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L17-L17)。

### [public void Save()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L77-L97)
将当前 [ConfigData](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L17-L17) 序列化为文本，并按照 [FileType](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L11-L11) 对应的方式写入文件。

### [public string? Dump()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L102-L112)
> 开发测试，随时会弃用，不建议实际应用

返回当前配置数据以字符串形式表示的内容。

---

## 枚举属性

### EnumFileType

| 属性     | 说明            |
|--------|---------------|
| Json   | JSON 配置文件类型   |
| Xml    | XMl 配置文件类型    | 
| Yaml   | YAML 配置文件类型   |
| Toml   | TOML 配置文件类型   |
| Binary | 二进制文件类型 (未实现) |

### EnumFormatType（未实现）

| 属性         | 说明            |
|------------|---------------|
| Default    | 默认格式          |
| PascalCase | 驼峰式命名，如 `AppName` |
| CamelCase  | 小驼峰式命名，如 `appName` |
| SnakeCase  | 下划线式命名，如 `app_name` |
| Constant   |常量命名，如 `APP_NAME` |


###

---

## 使用示例

### 1. 定义配置类
首先定义您的配置类，并且可设置默认数据，例如：

```csharp
public class AppConfig
{
    public string AppName { get; set; } = "MyApp";
    public int Port { get; set; } = 8080;
    public bool IsDebug { get; set; } = true;
    
    public TestData Test { get; set; } = new(); // 嵌套类，需要初始化构造
    public class TestData
    {
        public string A { get; set; } = "John Doe";
        public int B { get; set; } = 30;
    }
    
    public List<string> Users { get; set; } = new() { "Alice", "Bob", "Charlie" }; // 可存储列表等数据类型
}
```


### 2. 创建并绑定配置实例
```csharp
var config = new AppConfig();
var binding = new FileConfigBinding<AppConfig>("appsettings.yaml", config);
{
    FileType = FileConfigBinding<DataTest>.EnumFileType.Yaml, // 可选，默认json
    FormatType = FileConfigBinding<DataTest>.EnumFormatType.Default // 暂未实现
};
```


### 3. 初始化配置文件
如果文件尚未存在，调用 [Init()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L34-L44) 将自动创建文件并写入初始配置：
```csharp
binding.Init();
```


### 4. 加载文件配置
```csharp
binding.Load();
```


### 5. 修改并保存配置
修改 `config` 后，调用 [Save()](file://D:\Studio\.NET\MeowToolsLib\src\MeowToolsLib.Config\FileConfig\FileConfigBinding.cs#L77-L97) 持久化更改：
```csharp
config.Test.A = "下北泽特级厨师";
config.Port = 14514;
binding.Save();
```


### 6. 获取配置
```csharp
string configText = config.AppName;
Console.WriteLine(configText);
```


---

## 注意事项
- 如果保存或加载失败会抛出异常，需要自行进行异常处理。
- 文件类型需与数据格式兼容，例如 XML模式 不支持解析 TOML 内容。
- FormatType 目前未被使用，设置无效，后续可根据需求扩展其功能，如控制序列化风格。
- 不适用频繁修改并持久化的并发场景（有对应模块的开发计划，但暂未完成，咕咕咕）。

---

## 适用场景
- 需要持久化配置的轻量级应用。
- 支持多种配置文件格式的项目。
- 需要在运行时动态修改配置并保存的应用程序。
