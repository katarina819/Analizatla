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
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analitičara.
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Kontakt podaci analitičara (e-mail, telefon itd.).
        /// </summary>

        public string Kontakt { get; set; }

        /// <summary>
        /// Stručna sprema analitičara.
        /// </summary>
        public string StrucnaSprema { get; set; } = "";

        /// <summary>
        /// Kolekcija analiza koje je izvršio analitičar.
        /// Može biti null ako analitičar nema unesenih analiza.
        /// </summary>

        public ICollection<Analiza>?  Analize { get; set; }

        /// <summary>
        /// URL slike analitičara. Može biti null ako slika nije postavljena.
        /// </summary>
        public string? SlikaUrl { get; set; }

    }
}
