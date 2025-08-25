using System.ComponentModel.DataAnnotations;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO klasa koja se koristi za prijavu operatera.
    /// Sadrži obavezne podatke: Email i Lozinka.
    /// </summary>
    /// <param name="Email">Email adresa operatera. Obavezno polje.</param>
    /// <param name="Password">Lozinka operatera. Obavezno polje.</param>
    
    public record OperaterDTO(
       [Required(ErrorMessage = "Email je obavezan.")]
            string Email,
       [Required(ErrorMessage = "Lozinka je obavezna.")]
            string Password);
}