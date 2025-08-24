using System.ComponentModel.DataAnnotations;

namespace BACKEND.DTOs
{
    /// <summary>
    /// DTO za sliku.
    /// </summary>
    /// <param name="Base64">Base64 zapis slike (obavezno).</param>
    public record SlikaDTO(
        [Required(ErrorMessage = "Base64 zapis slike obavezno")] string Base64
    );
}
