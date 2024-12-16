// (c) Yury Malich, 2024-2025
// MIT License

using System;
using System.Text.Json;

using ValueStringType;

namespace ValueStringType.Converters;

/// <summary>
/// Json converter for System.Text. to read and write ValueStrings from/to JSON.
/// </summary>
/// <example>
/// <code>
/// var options = new System.Text.Json.JsonSerializerOptions();
/// options.Converters.Add(new JsonValueStringConverter());
/// var json = System.Text.Json.JsonSerializer.Serialize(myClass, options);
/// </code>
/// </example>

public sealed class JsonValueStringConverter : System.Text.Json.Serialization.JsonConverter<ValueString>
{
    public override ValueString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => ValueString.Empty,
            _ => reader.GetString(),
        };
    }

    public override void Write(Utf8JsonWriter writer, ValueString value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }

    public static JsonValueStringConverter Default = new();
}