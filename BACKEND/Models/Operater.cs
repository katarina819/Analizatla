using BACKEND.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja operatera sustava.
    /// Sadr�i osnovne podatke potrebne za prijavu.
    /// </summary>

    public class Operater : Entitet
    {
        /// <summary>
        /// Email adresa operatera. Koristi se za prijavu.
        /// </summary>
         

        [Column("email")]
        public string Email { get; set; } = "";

        /// <summary>
        /// Lozinka operatera. Koristi se za prijavu.
        /// </summary>

        [Column("lozinka")]
        public string Lozinka { get; set; } = "";
    }
}