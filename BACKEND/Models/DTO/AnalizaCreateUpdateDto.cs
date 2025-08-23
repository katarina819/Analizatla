using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    public class AnalizaCreateUpdateDto:Entitet
    {
        // Analiza
        [JsonPropertyName("datum")]
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? Datum { get; set; }

        [JsonPropertyName("pHVrijednost")]
        public decimal pHVrijednost { get; set; }

        [JsonPropertyName("Fosfor")]
        public decimal Fosfor { get; set; }

        [JsonPropertyName("Kalij")]
        public decimal Kalij { get; set; }

        [JsonPropertyName("Magnezij")]
        public decimal Magnezij { get; set; }

        [JsonPropertyName("Karbonati")]
        public decimal Karbonati { get; set; }

        [JsonPropertyName("Humus")]
        public decimal Humus { get; set; }

        // UzorciTla
        [JsonPropertyName("MasaUzorka")]
        public decimal MasaUzorka { get; set; }

        [JsonPropertyName("VrstaTla")]
        public string VrstaTla { get; set; }

        [JsonPropertyName("DatumUzorka")]
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? DatumUzorka { get; set; }

        [JsonPropertyName("MjestoUzorkovanja")]
        public string MjestoUzorkovanja { get; set; }

        // Analiticari
        [JsonPropertyName("Ime")]
        public string Ime { get; set; }

        [JsonPropertyName("Prezime")]
        public string Prezime { get; set; }

        [JsonPropertyName("Kontakt")]
        public string Kontakt { get; set; }

        [JsonPropertyName("StrucnaSprema")]
        public string StrucnaSprema { get; set; }
    }
}
