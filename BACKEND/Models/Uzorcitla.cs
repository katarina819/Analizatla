using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    [Table("uzorcitla")]
    public class Uzorcitla:Entitet
    {

        [Column("masauzorka")]
        public decimal MasaUzorka { get; set; }

        [Column("vrstatla")]
        public string VrstaTla { get; set; }

        [Column("datum")]
        public DateTime? Datum { get; set; }

        [Column("lokacija")]
        public int LokacijaId { get; set; }

        [ForeignKey("LokacijaId")]
        public Lokacija Lokacija { get; set; }

        

    }

}
