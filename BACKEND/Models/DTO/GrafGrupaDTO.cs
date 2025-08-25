namespace EdunovaApp.Models.DTO
{
    /// <summary>
    /// DTO objekt koji se koristi za prikaz podataka o uzorkovanju
    /// u grafovima ili izvještajima.
    /// </summary>
    /// <param name="MjestoUzorkovanja">
    /// Mjesto na kojem je uzorak uzet (npr. "Osijek – Vodocrpilište").
    /// </param>
    /// <param name="Datum">
    /// Datum kada je uzorkovanje zabilježeno u sustavu.
    /// Može biti null ako datum nije poznat.
    /// </param>
    /// <param name="DatumUzorka">
    /// Stvarni datum kada je uzorak prikupljen.
    /// Može biti null ako datum nije poznat.
    /// </param>
    public record GrafUzorcitlaDTO(
        string MjestoUzorkovanja,
        DateTime? Datum,
        DateTime? DatumUzorka
    );
}
