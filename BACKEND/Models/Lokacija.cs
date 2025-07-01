using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;


namespace BACKEND.Models
{
    public class Lokacija
    {
        [Key]
        public int Sifra { get; set; }
        public string mjestouzorkovanja { get; set; }
    }
}









