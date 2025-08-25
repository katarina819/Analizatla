using System.ComponentModel.DataAnnotations;

namespace BACKEND.Models
{
    /// <summary>
    /// Apstraktna baza za sve entitete u sustavu.
    /// Sadrži zajednièka svojstva koja nasljeðuju sve izvedene klase.
    /// </summary>
    public abstract class Entitet
    {
        /// <summary>
        /// Jedinstvena šifra (primarni kljuè) entiteta.
        /// </summary>
        [Key]
        public int Sifra { get; set; }
        
    }
}