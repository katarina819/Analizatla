using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;


namespace BACKEND.Models
{
    public class Lokacija:Entitet
    {
        
        public string MjestoUzorkovanja { get; set; } = "";

        [JsonIgnore]
        public ICollection<Uzorcitla>? UzorciTla { get; set; }

    }
}









