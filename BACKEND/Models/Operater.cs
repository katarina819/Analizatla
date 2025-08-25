using BACKEND.Models;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja operatera sustava.
    /// Sadrži osnovne podatke potrebne za prijavu.
    /// </summary>

    public class Operater : Entitet
    {
        /// <summary>
        /// Email adresa operatera. Koristi se za prijavu.
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Lozinka operatera. Koristi se za prijavu.
        /// </summary>
        public string Lozinka { get; set; } = "";
    }
}