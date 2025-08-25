namespace EdunovaApp.Models.DTO
{
    /// <summary>
    /// DTO objekt koji se koristi za prikaz podataka o uzorkovanju
    /// u grafovima ili izvje�tajima.
    /// </summary>
    /// <param name="MjestoUzorkovanja">
    /// Mjesto na kojem je uzorak uzet (npr. "Osijek � Vodocrpili�te").
    /// </param>
    /// <param name="Datum">
    /// Datum kada je uzorkovanje zabilje�eno u sustavu.
    /// Mo�e biti null ako datum nije poznat.
    /// </param>
    /// <param name="DatumUzorka">
    /// Stvarni datum kada je uzorak prikupljen.
    /// Mo�e biti null ako datum nije poznat.
    /// </param>
    public record GrafUzorcitlaDTO(
        string MjestoUzorkovanja,
        DateTime? Datum,
        DateTime? DatumUzorka
    );
}
