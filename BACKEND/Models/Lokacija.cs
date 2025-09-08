using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja lokaciju uzorkovanja tla.
    /// </summary>
    public class Lokacija:Entitet
    {
        /// <summary>
        /// Naziv mjesta uzorkovanja.
        /// </summary>

        [Column("mjestouzorkovanja")]
        public string MjestoUzorkovanja { get; set; } = "";

        /// <summary>
        /// Kolekcija uzoraka tla prikupljenih na ovoj lokaciji.
        /// Oznaka [JsonIgnore] sprječava serijalizaciju ovog svojstva u JSON.
        /// Može biti null ako nema unesenih uzoraka.
        /// </summary>
        [JsonIgnore]
        public ICollection<Uzorcitla>? UzorciTla { get; set; }

      

    }
}









