using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;



namespace BACKEND.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje analizama tla.
    /// Omogu�uje CRUD operacije nad analizama, uklju�uju�i dohvat, dodavanje, a�uriranje i brisanje.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnalizaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        /// <summary>
        /// Konstruktor kontrolera.
        /// </summary>
        /// <param name="context">Instanca <see cref="EdunovaContext"/> za pristup bazi podataka.</param>
        public AnalizaController(EdunovaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohva�a sve analize iz baze.
        /// </summary>
        /// <returns>Lista svih analiza s pripadaju�im uzorcima tla i analiti�arima.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // 1. Dohvati podatke iz baze sa Include-ovima
                var rawData = _context.Analize
                    .Include(a => a.UzorakTla)
                        .ThenInclude(u => u.Lokacija)
                    .Include(a => a.Analiticar)
                    .ToList(); // <-- podaci sada u memoriji

                // 2. Mapiranje DTO sigurno u memoriji
                var podaci = rawData.Select(a => new AnalizaDto
                {
                    Sifra = a.Sifra,
                    Datum = a.Datum,
                    pHVrijednost = a.pHVrijednost,
                    Fosfor = a.Fosfor,
                    Kalij = a.Kalij,
                    Magnezij = a.Magnezij,
                    Karbonati = a.Karbonati,
                    Humus = a.Humus,

                    MasaUzorka = a.UzorakTla != null ? a.UzorakTla.MasaUzorka : 0,
                    VrstaTla = a.UzorakTla != null ? a.UzorakTla.VrstaTla : "",
                    DatumUzorka = a.UzorakTla?.Datum,
                    MjestoUzorkovanja = a.UzorakTla?.Lokacija?.MjestoUzorkovanja ?? "",
                    Ime = a.Analiticar?.Ime ?? "",
                    Prezime = a.Analiticar?.Prezime ?? "",
                    Kontakt = a.Analiticar?.Kontakt ?? "",
                    StrucnaSprema = a.Analiticar?.StrucnaSprema ?? ""
                }).ToList();

                return Ok(podaci);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { poruka = e.Message });
            }
        }



        /// <summary>
        /// Dohva�a analizu po �ifri.
        /// </summary>
        /// <param name="sifra">Jedinstvena �ifra analize.</param>
        /// <returns>Objekt <see cref="AnalizaDto"/> ako postoji, 404 NotFound ina�e.</returns>
        [HttpGet("{sifra:int}")]
        public IActionResult Get(int sifra)
        {
            if (sifra <= 0)
                return BadRequest("�ifra mora biti ve�a od 0");

            try
            {
                var analiza = _context.Analize
                .Include(a => a.UzorakTla)
                    .ThenInclude(u => u.Lokacija)
                .Include(a => a.Analiticar)
                .Where(a => a.Sifra == sifra)
                .Select(a => new AnalizaDto
                {
                    Sifra = a.Sifra,
                    Datum = a.Datum,
                    pHVrijednost = a.pHVrijednost,
                    Fosfor = a.Fosfor,
                    Kalij = a.Kalij,
                    Magnezij = a.Magnezij,
                    Karbonati = a.Karbonati,
                    Humus = a.Humus,

                    MasaUzorka = a.UzorakTla.MasaUzorka,
                    VrstaTla = a.UzorakTla.VrstaTla,
                    DatumUzorka = a.UzorakTla.Datum,
                    MjestoUzorkovanja = a.UzorakTla.Lokacija.MjestoUzorkovanja, // <� ispravljeno

                    Ime = a.Analiticar.Ime,
                    Prezime = a.Analiticar.Prezime,
                    Kontakt = a.Analiticar.Kontakt,
                    StrucnaSprema = a.Analiticar.StrucnaSprema
                })
                .FirstOrDefault();


                if (analiza == null)
                    return NotFound();

                return Ok(analiza);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Dodaje novu analizu u bazu.
        /// </summary>
        /// <param name="dto">DTO objekt <see cref="AnalizaCreateUpdateDto"/> s podacima za kreiranje analize.</param>
        /// <returns>Stvoreni objekt <see cref="AnalizaDto"/> s HTTP status 201 Created.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] AnalizaCreateUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Payload ne smije biti null");

            try
            {
                var analiza = new Analiza
                {
                    Datum = dto.Datum ?? DateTime.Now,
                    pHVrijednost = dto.pHVrijednost,
                    Fosfor = dto.Fosfor,
                    Kalij = dto.Kalij,
                    Magnezij = dto.Magnezij,
                    Karbonati = dto.Karbonati,
                    Humus = dto.Humus,
                    UzorakTla = new Uzorcitla
                    {
                        MasaUzorka = dto.MasaUzorka,
                        VrstaTla = dto.VrstaTla ?? "",
                        Datum = dto.DatumUzorka ?? DateTime.Now,
                        Lokacija = new Lokacija
                        {
                            MjestoUzorkovanja = dto.MjestoUzorkovanja ?? ""
                        }
                    },
                    Analiticar = new Analiticar
                    {
                        Ime = dto.Ime ?? "",
                        Prezime = dto.Prezime ?? "",
                        Kontakt = dto.Kontakt ?? "",
                        StrucnaSprema = dto.StrucnaSprema ?? ""
                    }
                };

                _context.Analize.Add(analiza);
                _context.SaveChanges();

                // Vra�amo DTO
                var result = new AnalizaDto
                {
                    Sifra = analiza.Sifra,
                    Datum = analiza.Datum,
                    pHVrijednost = analiza.pHVrijednost,
                    Fosfor = analiza.Fosfor,
                    Kalij = analiza.Kalij,
                    Magnezij = analiza.Magnezij,
                    Karbonati = analiza.Karbonati,
                    Humus = analiza.Humus,
                    MasaUzorka = analiza.UzorakTla.MasaUzorka,
                    VrstaTla = analiza.UzorakTla.VrstaTla,
                    DatumUzorka = analiza.UzorakTla.Datum,
                    MjestoUzorkovanja = analiza.UzorakTla.Lokacija.MjestoUzorkovanja,
                    Ime = analiza.Analiticar.Ime,
                    Prezime = analiza.Analiticar.Prezime,
                    Kontakt = analiza.Analiticar.Kontakt,
                    StrucnaSprema = analiza.Analiticar.StrucnaSprema
                };

                return CreatedAtAction(nameof(Get), new { sifra = analiza.Sifra }, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }



        /// <summary>
        /// A�urira postoje�u analizu.
        /// </summary>
        /// <param name="sifra">�ifra analize koja se a�urira.</param>
        /// <param name="dto">DTO objekt <see cref="AnalizaCreateUpdateDto"/> s novim podacima.</param>
        /// <returns>HTTP 204 NoContent ako je uspje�no a�urirano.</returns>
        // PUT: a�uriranje postoje�e analize
        [HttpPut("{sifra:int}")]
        public IActionResult Put(int sifra, [FromBody] AnalizaCreateUpdateDto dto)
        {
            var analiza = _context.Analize
                .Include(a => a.UzorakTla)
                    .ThenInclude(u => u.Lokacija)
                .Include(a => a.Analiticar)
                .FirstOrDefault(a => a.Sifra == sifra);

            if (analiza == null)
                return NotFound();

            // Update Analize
            analiza.Datum = dto.Datum;
            analiza.pHVrijednost = dto.pHVrijednost;
            analiza.Fosfor = dto.Fosfor;
            analiza.Kalij = dto.Kalij;
            analiza.Magnezij = dto.Magnezij;
            analiza.Karbonati = dto.Karbonati;
            analiza.Humus = dto.Humus;

            // Update UzorkaTla
            analiza.UzorakTla.MasaUzorka = dto.MasaUzorka;
            analiza.UzorakTla.VrstaTla = dto.VrstaTla;
            analiza.UzorakTla.Datum = dto.DatumUzorka;
            analiza.UzorakTla.Lokacija.MjestoUzorkovanja = dto.MjestoUzorkovanja;

            // Update Analiticara
            analiza.Analiticar.Ime = dto.Ime;
            analiza.Analiticar.Prezime = dto.Prezime;
            analiza.Analiticar.Kontakt = dto.Kontakt;
            analiza.Analiticar.StrucnaSprema = dto.StrucnaSprema;

            _context.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// Bri�e analizu po �ifri.
        /// </summary>
        /// <param name="sifra">�ifra analize koja se bri�e.</param>
        /// <returns>Poruka o uspjehu ili neuspjehu brisanja.</returns>
        // DELETE: brisanje analize
        [HttpDelete("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            if (sifra < 1)
                return BadRequest(new { poruka = "�ifra mora biti ve�a od 0" });

            try
            {
                var analiza = _context.Analize.Find(sifra);
                if (analiza == null)
                    return NotFound(new { poruka = "Analiza ne postoji" });

                _context.Analize.Remove(analiza);
                _context.SaveChanges();

                // Ako brisanje uspije
                return Ok(new { poruka = "Analiza je uspje�no obrisana." });
            }
            catch (DbUpdateException)
            {
                // Ako postoji foreign key constraint koji sprje�ava brisanje
                return BadRequest(new { poruka = "Ne mo�ete obrisati ovu analizu jer je povezana s drugim podacima." });
            }
            catch (Exception ex)
            {
                // Ostale neo�ekivane gre�ke
                return BadRequest(new { poruka = $"Dogodila se neo�ekivana gre�ka: {ex.Message}" });
            }
        }

    }
}

