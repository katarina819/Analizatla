using System.Text.Json.Serialization;
using BACKEND.Models;

public class UzorcitlaDto : Entitet
{
    [JsonConverter(typeof(JsonNullableDateConverter))]
    public DateTime? Datum { get; set; }

    public decimal MasaUzorka { get; set; }
    public string VrstaTla { get; set; }

    public string MjestoUzorkovanja { get; set; }

}
