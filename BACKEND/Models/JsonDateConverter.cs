using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonNullableDateConverter : JsonConverter<DateTime?>
{
    private readonly string _format = "yyyy-MM-dd"; 

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (string.IsNullOrEmpty(str))
            return null;

        // Parse ISO 8601 string
        return DateTime.Parse(str, null, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(_format)); 
        else
            writer.WriteNullValue();
    }
}


