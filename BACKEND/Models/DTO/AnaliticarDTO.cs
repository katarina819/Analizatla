namespace BACKEND.Models.DTO
{

    /// <summary>
    /// Data Transfer Object (DTO) za entitet Analiti�ar.
    /// Koristi se za prijenos podataka izme�u backend-a i klijenta.
    /// </summary>
    public class AnaliticarDTO
    {

        /// <summary>
        /// Jedinstvena �ifra analiti�ara.
        /// </summary>
        /// 
        public int Sifra { get; set; }

        /// <summary>
        /// Ime analiti�ara.
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime analiti�ara.
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

        /// <summary>
        /// URL slike analiti�ara (mo�e biti null).
        /// </summary>
        public string? SlikaUrl { get; set; }
    }
}
