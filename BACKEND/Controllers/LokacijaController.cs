using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BACKEND.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class LokacijaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        public LokacijaController(EdunovaContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Lokacije);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{sifra:int}")]
        public IActionResult Get(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest("Šifra nije dobra");
            }
            try
            {
                var lokacija = _context.Lokacije.Find(sifra);
                if (lokacija == null)
                {
                    return NotFound();
                }
                return Ok(lokacija);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



        [HttpPost]

        public IActionResult Post(Lokacija lokacija)
        {
            try
            {
                _context.Lokacije.Add(lokacija);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, lokacija);


            }

            catch (Exception e)
            {
                return BadRequest(e);



            }



        }

        [HttpPut("{sifra:int}")]
        public IActionResult Put(int sifra, Lokacija lokacija)
        {
            if (sifra < 1)
            {
                return BadRequest(new { poruka = "Šifra mora biti veća od 0" });
            }

            try
            {
                Lokacija l = _context.Lokacije.Find(sifra);
                if (l == null)
                {
                    return NotFound();
                }

                l.MjestoUzorkovanja = lokacija.MjestoUzorkovanja;

                _context.Lokacije.Update(l);
                _context.SaveChanges();
                return Ok(l);
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{sifra:int}")]
        public async Task<IActionResult> Delete(int sifra)
        {
            if (sifra < 1)
                return BadRequest(new { poruka = "Šifra mora biti veća od 0" });

            var lokacija = await _context.Lokacije.FindAsync(sifra);
            if (lokacija == null)
                return NotFound(new { poruka = "Lokacija s tom šifrom ne postoji" });

            try
            {
                _context.Lokacije.Remove(lokacija);
                await _context.SaveChangesAsync();

                // Ako brisanje uspije
                return Ok(new { poruka = "Lokacija je uspješno obrisana." });
            }
            catch (DbUpdateException ex)
            {
                // Ako postoji foreign key constraint koji sprječava brisanje
                return BadRequest(new
                {
                    poruka = "Ne možete obrisati lokaciju koja je u upotrebi.",
                    detalji = ex.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                // Ostale neočekivane greške
                return BadRequest(new { poruka = $"Dogodila se neočekivana greška: {ex.Message}" });
            }
        }


    }
}
