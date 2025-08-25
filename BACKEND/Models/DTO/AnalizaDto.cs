using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO klasa za prikaz podataka o Analizi, UzorcimaTla i Analitièarima.
    /// Koristi se za slanje podataka prema klijentu.
    /// </summary>
    public class AnalizaDto:Entitet
    {
        // Analiza
        /// <summary>
        /// Datum kada je analiza izvršena. Može biti null.
        /// </summary>
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// pH vrijednost tla.
        /// </summary>
        public decimal pHVrijednost { get; set; }

        /// <summary>
        /// Sadržaj fosfora u tlu.
        /// </summary>
        public decimal Fosfor { get; set; }

        /// <summary>
        /// Sadržaj kalija u tlu.
        /// </summary>
        public decimal Kalij { get; set; }

        /// <summary>
        /// Sadržaj magnezija u tlu.
        /// </summary>
        public decimal Magnezij { get; set; }

        /// <summary>
        /// Sadržaj karbonata u tlu.
        /// </summary>
        public decimal Karbonati { get; set; }

        /// <summary>
        /// Sadržaj humusa u tlu.
        /// </summary>
        public decimal Humus { get; set; }

        
        // UzorciTla
        /// <summary>
        /// Masa uzorka tla.
        /// </summary>
        public decimal MasaUzorka { get; set; }

        /// <summary>
        /// Vrsta tla (npr. glina, pijesak, ilovaèa).
        /// </summary>
        public string VrstaTla { get; set; }

        /// <summary>
        /// Datum uzimanja uzorka. Može biti null.
        /// </summary>
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? DatumUzorka { get; set; }

        /// <summary>
        /// Mjesto uzorkovanja tla.
        /// </summary>
        public string MjestoUzorkovanja { get; set; }

        
        // Analiticari
        /// <summary>
        /// Ime analitièara koji je izvršio analizu.
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analitièara koji je izvršio analizu.
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analitièara (e-mail, telefon i sl.).
        /// </summary>
        public string Kontakt { get; set; }

        /// <summary>
        /// Struèna sprema analitièara.
        /// </summary>
        public string StrucnaSprema { get; set; }
    }
}
