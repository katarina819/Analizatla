using System;
using System.Text.Json.Serialization;
using BACKEND.Models;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO klasa za prikaz podataka o Analizi, UzorcimaTla i Analiti�arima.
    /// Koristi se za slanje podataka prema klijentu.
    /// </summary>
    public class AnalizaDto:Entitet
    {
        // Analiza
        /// <summary>
        /// Datum kada je analiza izvr�ena. Mo�e biti null.
        /// </summary>
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// pH vrijednost tla.
        /// </summary>
        public decimal pHVrijednost { get; set; }

        /// <summary>
        /// Sadr�aj fosfora u tlu.
        /// </summary>
        public decimal Fosfor { get; set; }

        /// <summary>
        /// Sadr�aj kalija u tlu.
        /// </summary>
        public decimal Kalij { get; set; }

        /// <summary>
        /// Sadr�aj magnezija u tlu.
        /// </summary>
        public decimal Magnezij { get; set; }

        /// <summary>
        /// Sadr�aj karbonata u tlu.
        /// </summary>
        public decimal Karbonati { get; set; }

        /// <summary>
        /// Sadr�aj humusa u tlu.
        /// </summary>
        public decimal Humus { get; set; }

        
        // UzorciTla
        /// <summary>
        /// Masa uzorka tla.
        /// </summary>
        public decimal MasaUzorka { get; set; }

        /// <summary>
        /// Vrsta tla (npr. glina, pijesak, ilova�a).
        /// </summary>
        public string VrstaTla { get; set; }

        /// <summary>
        /// Datum uzimanja uzorka. Mo�e biti null.
        /// </summary>
        [JsonConverter(typeof(JsonNullableDateConverter))]
        public DateTime? DatumUzorka { get; set; }

        /// <summary>
        /// Mjesto uzorkovanja tla.
        /// </summary>
        public string MjestoUzorkovanja { get; set; }

        
        // Analiticari
        /// <summary>
        /// Ime analiti�ara koji je izvr�io analizu.
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analiti�ara koji je izvr�io analizu.
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analiti�ara (e-mail, telefon i sl.).
        /// </summary>
        public string Kontakt { get; set; }

        /// <summary>
        /// Stru�na sprema analiti�ara.
        /// </summary>
        public string StrucnaSprema { get; set; }
    }
}
