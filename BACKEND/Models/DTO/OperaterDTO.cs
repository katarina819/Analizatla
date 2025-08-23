using System.ComponentModel.DataAnnotations;

namespace BACKEND.DTOs
{
    public record OperaterDTO(
       [Required(ErrorMessage = "Email je obavezan.")]
            string Email,
       [Required(ErrorMessage = "Lozinka je obavezna.")]
            string Password);
}