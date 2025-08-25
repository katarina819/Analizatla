namespace BACKEND.Models.DTO
{

    /// <summary>
    /// Data Transfer Object (DTO) za entitet Analitièar.
    /// Koristi se za prijenos podataka izmeðu backend-a i klijenta.
    /// </summary>
    public class AnaliticarDTO
    {

        /// <summary>
        /// Jedinstvena šifra analitièara.
        /// </summary>
        /// 
        public int Sifra { get; set; }

        /// <summary>
        /// Ime analitièara.
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analitièara.
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

        /// <summary>
        /// URL slike analitièara (može biti null).
        /// </summary>
        public string? SlikaUrl { get; set; }
    }
}
