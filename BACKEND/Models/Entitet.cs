using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    /// <summary>
    /// Apstraktna baza za sve entitete u sustavu.
    /// Sadr�i zajedni�ka svojstva koja naslje�uju sve izvedene klase.
    /// </summary>
    public abstract class Entitet
    {
        /// <summary>
        /// Jedinstvena �ifra (primarni klju�) entiteta.
        /// </summary>
        [Key]
        [Column("sifra")]
        public int Sifra { get; set; }
        
    }
}