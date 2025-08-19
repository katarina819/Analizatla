using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    public class AnalizaCreateUpdateDto:Entitet
    {
        // Analiza
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? Datum { get; set; }
        public decimal pHVrijednost { get; set; }
        public decimal Fosfor { get; set; }
        public decimal Kalij { get; set; }
        public decimal Magnezij { get; set; }
        public decimal Karbonati { get; set; }
        public decimal Humus { get; set; }

        // UzorciTla
        public decimal MasaUzorka { get; set; }
        public string VrstaTla { get; set; }

        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? DatumUzorka { get; set; }

        public string MjestoUzorkovanja { get; set; }

        // Analiticari
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string StrucnaSprema { get; set; }
    }
}
