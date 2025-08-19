using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public class Analiza:Entitet
    {
       
        public DateTime? Datum { get; set; }


        [Column("uzoraktla")]
        public int UzorakTlaId { get; set; }


        [ForeignKey(nameof(UzorakTlaId))]
        public Uzorcitla UzorakTla { get; set; }


        [Column("analiticar")]
        public int AnaliticarId { get; set; }

        [ForeignKey(nameof(AnaliticarId))]
        public Analiticar Analiticar { get; set; }


        public decimal pHVrijednost { get; set; }

        
        public decimal Fosfor { get; set; }

        
        public decimal Kalij { get; set; }

        
        public decimal Magnezij { get; set; }

        
        public decimal Karbonati { get; set; }

        
        public decimal Humus { get; set; }

       
    }
}
