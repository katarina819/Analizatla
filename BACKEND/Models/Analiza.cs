using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja analizu tla.
    /// Sadrži podatke o uzorku tla, analitičaru koji je izvršio analizu
    /// te kemijske i fizikalne parametre tla.
    /// </summary>
    public class Analiza:Entitet
    {
        /// <summary>
        /// Datum kada je analiza izvršena. Može biti null.
        /// </summary>

        [Column("datum")]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// ID uzorka tla kojem analiza pripada.
        /// </summary>
        [Column("uzoraktla")]
        public int UzorakTlaId { get; set; }

        /// <summary>
        /// Referenca na entitet Uzorcitla.
        /// </summary>
        [ForeignKey(nameof(UzorakTlaId))]
        public Uzorcitla UzorakTla { get; set; }

        /// <summary>
        /// ID analitičara koji je izvršio analizu.
        /// </summary>
        [Column("analiticar")]
        public int AnaliticarId { get; set; }

        /// <summary>
        /// Referenca na entitet Analiticar.
        /// </summary>
        [ForeignKey(nameof(AnaliticarId))]
        public Analiticar Analiticar { get; set; }

        /// <summary>
        /// pH vrijednost tla.
        /// </summary>

        [Column("pHvrijednost")]
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

       
    }
}
