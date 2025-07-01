using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public class Uzorcitla
    {
        
        public int Sifra { get; set; }

        
        public decimal MasaUzorka { get; set; }

        
        public string VrstaTla { get; set; }

        public DateTime? Datum { get; set; }

        
       

       
    }
}
