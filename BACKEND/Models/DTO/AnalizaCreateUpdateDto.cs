using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO klasa za kreiranje ili a�uriranje Analize i povezanih podataka o UzorcimaTla i Analiti�arima.
    /// </summary>
    public class AnalizaCreateUpdateDto:Entitet
    {

        
        /// <summary>
        /// Datum kada je analiza izvr�ena. Mo�e biti null.
        /// </summary>
        // Analiza
        [JsonPropertyName("datum")]
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// pH vrijednost tla.
        /// </summary>
        [JsonPropertyName("pHVrijednost")]
        public decimal pHVrijednost { get; set; }

        /// <summary>
        /// Sadr�aj fosfora u tlu.
        /// </summary>
        [JsonPropertyName("Fosfor")]
        public decimal Fosfor { get; set; }

        /// <summary>
        /// Sadr�aj kalija u tlu.
        /// </summary>
        [JsonPropertyName("Kalij")]
        public decimal Kalij { get; set; }

        /// <summary>
        /// Sadr�aj magnezija u tlu.
        /// </summary>
        [JsonPropertyName("Magnezij")]
        public decimal Magnezij { get; set; }

        /// <summary>
        /// Sadr�aj karbonata u tlu.
        /// </summary>
        [JsonPropertyName("Karbonati")]
        public decimal Karbonati { get; set; }

        /// <summary>
        /// Sadr�aj humusa u tlu.
        /// </summary>
        [JsonPropertyName("Humus")]
        public decimal Humus { get; set; }

        
        /// <summary>
        /// Masa uzorka tla.
        /// </summary>
        // UzorciTla
        [JsonPropertyName("MasaUzorka")]
        public decimal MasaUzorka { get; set; }

        /// <summary>
        /// Vrsta tla (npr. glina, pijesak, ilova�a).
        /// </summary>
        [JsonPropertyName("VrstaTla")]
        public string VrstaTla { get; set; }

        /// <summary>
        /// Datum uzimanja uzorka. Mo�e biti null.
        /// </summary>
        [JsonPropertyName("DatumUzorka")]
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? DatumUzorka { get; set; }

        /// <summary>
        /// Mjesto uzorkovanja tla.
        /// </summary>
        [JsonPropertyName("MjestoUzorkovanja")]
        public string MjestoUzorkovanja { get; set; }

        
        /// <summary>
        /// Ime analiti�ara koji je izvr�io analizu.
        /// </summary>
        // Analiticari
        [JsonPropertyName("Ime")]
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analiti�ara koji je izvr�io analizu.
        /// </summary>
        [JsonPropertyName("Prezime")]
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analiti�ara (e-mail, telefon i sl.).
        /// </summary>
        [JsonPropertyName("Kontakt")]
        public string Kontakt { get; set; }

        /// <summary>
        /// Stru�na sprema analiti�ara.
        /// </summary>
        [JsonPropertyName("StrucnaSprema")]
        public string StrucnaSprema { get; set; }
    }
}
