using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Konverter za serijalizaciju i deserijalizaciju <see cref="DateTime?"/> vrijednosti
/// u JSON-u koristeæi format "yyyy-MM-dd".
/// </summary>
public class JsonNullableDateConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// Format datuma koji se koristi prilikom serijalizacije u JSON.
    /// </summary>

    private readonly string _format = "yyyy-MM-dd";

    /// <summary>
    /// Deserijalizira string iz JSON-a u <see cref="DateTime?"/> vrijednost.
    /// </summary>
    /// <param name="reader">Reader za èitanje JSON tokena.</param>
    /// <param name="typeToConvert">Tip koji se konvertira (DateTime?).</param>
    /// <param name="options">Opcije serijalizacije.</param>
    /// <returns>Vraæa <see cref="DateTime?"/> ili null ako je string prazan.</returns>
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (string.IsNullOrEmpty(str))
            return null;

        // Parse ISO 8601 string
        return DateTime.Parse(str, null, DateTimeStyles.RoundtripKind);
    }

    /// <summary>
    /// Serijalizira <see cref="DateTime?"/> vrijednost u JSON koristeæi zadani format.
    /// </summary>
    /// <param name="writer">Writer za pisanje JSON tokena.</param>
    /// <param name="value">Vrijednost datuma koja se serijalizira.</param>
    /// <param name="options">Opcije serijalizacije.</param>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(_format)); 
        else
            writer.WriteNullValue();
    }
}


