using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;



namespace BACKEND.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnalizaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        public AnalizaController(EdunovaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var podaci = _context.Analize
                .Include(a => a.UzorakTla)
                    .ThenInclude(u => u.Lokacija)
                .Include(a => a.Analiticar)
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
                    MjestoUzorkovanja = a.UzorakTla.Lokacija.MjestoUzorkovanja, // ispravljeno

                    Ime = a.Analiticar.Ime,
                    Prezime = a.Analiticar.Prezime,
                    Kontakt = a.Analiticar.Kontakt,
                    StrucnaSprema = a.Analiticar.StrucnaSprema
                })
                .ToList();

                return Ok(podaci);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{sifra:int}")]
        public IActionResult Get(int sifra)
        {
            if (sifra <= 0)
                return BadRequest("Šifra mora biti veæa od 0");

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
                    MjestoUzorkovanja = a.UzorakTla.Lokacija.MjestoUzorkovanja, // <— ispravljeno

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

                // Vraæamo DTO
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




        // PUT: ažuriranje postojeæe analize
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



        // DELETE: brisanje analize
        [HttpDelete("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            try
            {
                var analiza = _context.Analize.Find(sifra);
                if (analiza == null)
                    return NotFound();

                _context.Analize.Remove(analiza);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

