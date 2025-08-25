using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BACKEND.Models
{
    /// <summary>
    /// Entitet koji predstavlja uzorak tla.
    /// Sadrži informacije o masi uzorka, vrsti tla, datumu prikupljanja i lokaciji.
    /// </summary>
    [Table("uzorcitla")]
    public class Uzorcitla:Entitet
    {
        /// <summary>
        /// Masa uzorka tla.
        /// </summary>
        [Column("masauzorka")]
        public decimal MasaUzorka { get; set; }

        /// <summary>
        /// Vrsta tla (npr. glina, pijesak, ilovača).
        /// </summary>
        [Column("vrstatla")]
        public string VrstaTla { get; set; }

        /// <summary>
        /// Datum kada je uzorak prikupljen. Može biti null.
        /// </summary>
        [Column("datum")]
        public DateTime? Datum { get; set; }

        /// <summary>
        /// ID lokacije na kojoj je uzorak tla prikupljen.
        /// </summary>
        [Column("lokacija")]
        public int LokacijaId { get; set; }

        /// <summary>
        /// Referenca na entitet Lokacija.
        /// </summary>
        [ForeignKey("LokacijaId")]
        public Lokacija Lokacija { get; set; }

        /// <summary>
        /// Kolekcija analiza koje su izvršene na ovom uzorku tla.
        /// Oznaka [JsonIgnore] sprječava serijalizaciju ovog svojstva u JSON.
        /// Može biti null ako nema unesenih analiza.
        /// </summary>
        [JsonIgnore]
        public ICollection<Analiza>? Analize { get; set; }




    }

}
