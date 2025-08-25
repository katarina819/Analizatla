using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO klasa za kreiranje ili ažuriranje Analize i povezanih podataka o UzorcimaTla i Analitièarima.
    /// </summary>
    public class AnalizaCreateUpdateDto:Entitet
    {

        
        /// <summary>
        /// Datum kada je analiza izvršena. Može biti null.
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
        /// Sadržaj fosfora u tlu.
        /// </summary>
        [JsonPropertyName("Fosfor")]
        public decimal Fosfor { get; set; }

        /// <summary>
        /// Sadržaj kalija u tlu.
        /// </summary>
        [JsonPropertyName("Kalij")]
        public decimal Kalij { get; set; }

        /// <summary>
        /// Sadržaj magnezija u tlu.
        /// </summary>
        [JsonPropertyName("Magnezij")]
        public decimal Magnezij { get; set; }

        /// <summary>
        /// Sadržaj karbonata u tlu.
        /// </summary>
        [JsonPropertyName("Karbonati")]
        public decimal Karbonati { get; set; }

        /// <summary>
        /// Sadržaj humusa u tlu.
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
        /// Vrsta tla (npr. glina, pijesak, ilovaèa).
        /// </summary>
        [JsonPropertyName("VrstaTla")]
        public string VrstaTla { get; set; }

        /// <summary>
        /// Datum uzimanja uzorka. Može biti null.
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
        /// Ime analitièara koji je izvršio analizu.
        /// </summary>
        // Analiticari
        [JsonPropertyName("Ime")]
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analitièara koji je izvršio analizu.
        /// </summary>
        [JsonPropertyName("Prezime")]
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analitièara (e-mail, telefon i sl.).
        /// </summary>
        [JsonPropertyName("Kontakt")]
        public string Kontakt { get; set; }

        /// <summary>
        /// Struèna sprema analitièara.
        /// </summary>
        [JsonPropertyName("StrucnaSprema")]
        public string StrucnaSprema { get; set; }
    }
}
