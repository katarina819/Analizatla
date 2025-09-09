using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja analitičara.
    /// Sadrži osnovne podatke o analitičaru i njegove analize.
    /// </summary>
    public class Analiticar:Entitet
    {
        /// <summary>
        /// Ime analitičara.
        /// </summary>

        [Column("ime")]
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analitičara.
        /// </summary>

        [Column("prezime")]
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analitičara (e-mail, telefon itd.).
        /// </summary>


        [Column("kontakt")]
        public string Kontakt { get; set; }

        /// <summary>
        /// Stručna sprema analitičara.
        /// </summary>

        [Column("strucnaSprema")]
        public string StrucnaSprema { get; set; } = "";

        /// <summary>
        /// Kolekcija analiza koje je izvršio analitičar.
        /// Može biti null ako analitičar nema unesenih analiza.
        /// </summary>

        public ICollection<Analiza>?  Analize { get; set; }

        /// <summary>
        /// URL slike analitičara. Može biti null ako slika nije postavljena.
        /// </summary>

        [Column("slikaUrl")]
        public string? SlikaUrl { get; set; }

    }
}
