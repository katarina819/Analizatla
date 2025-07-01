using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public class Analiza
    {
       
        public int Sifra { get; set; }

        public DateTime? Datum { get; set; }

        
        public int UzorakTla { get; set; }

        
        public int Analiticar { get; set; }

        
        public decimal pHVrijednost { get; set; }

        
        public decimal Fosfor { get; set; }

        
        public decimal Kalij { get; set; }

        
        public decimal Magnezij { get; set; }

        
        public decimal Karbonati { get; set; }

        
        public decimal Humus { get; set; }

       
    }
}
