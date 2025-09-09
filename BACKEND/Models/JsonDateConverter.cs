using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Konverter za serijalizaciju i deserijalizaciju <see cref="DateTime?"/> vrijednosti
/// u JSON-u koriste�i format "yyyy-MM-dd".
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
    /// <param name="reader">Reader za �itanje JSON tokena.</param>
    /// <param name="typeToConvert">Tip koji se konvertira (DateTime?).</param>
    /// <param name="options">Opcije serijalizacije.</param>
    /// <returns>Vra�a <see cref="DateTime?"/> ili null ako je string prazan.</returns>
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (string.IsNullOrEmpty(str))
            return null;

        // Parse ISO string i prisili UTC
        var dt = DateTime.Parse(str, null, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
    }

    /// <summary>
    /// Serijalizira <see cref="DateTime?"/> vrijednost u JSON koriste�i zadani format.
    /// </summary>
    /// <param name="writer">Writer za pisanje JSON tokena.</param>
    /// <param name="value">Vrijednost datuma koja se serijalizira.</param>
    /// <param name="options">Opcije serijalizacije.</param>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            // Prisiljava UTC prije zapisivanja
            var utcValue = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
            writer.WriteStringValue(utcValue.ToString(_format));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}


