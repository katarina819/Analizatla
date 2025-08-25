using System.Text.Json.Serialization;
using BACKEND.Models;

/// <summary>
/// DTO klasa koja predstavlja podatke o uzorku tla.
/// Koristi se za prijenos podataka izme�u backend-a i klijenta.
/// </summary>

public class UzorcitlaDto : Entitet
{
    /// <summary>
    /// Datum kada je uzorak tla prikupljen. Mo�e biti null.
    /// </summary>
    [JsonConverter(typeof(JsonNullableDateConverter))]
    public DateTime? Datum { get; set; }

    /// <summary>
    /// Masa uzorka tla.
    /// </summary>
    public decimal MasaUzorka { get; set; }

    /// <summary>
    /// Vrsta tla (npr. glina, pijesak, ilova�a).
    /// </summary>
    public string VrstaTla { get; set; }

    /// <summary>
    /// Mjesto gdje je uzorak tla prikupljen.
    /// </summary>

    public string MjestoUzorkovanja { get; set; }

}
