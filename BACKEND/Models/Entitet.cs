using System.ComponentModel.DataAnnotations;

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
        public int Sifra { get; set; }
        
    }
}