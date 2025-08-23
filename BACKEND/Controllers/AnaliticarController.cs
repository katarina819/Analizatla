using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BACKEND.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class AnaliticarController : ControllerBase
    {
        private readonly EdunovaContext _context;

        public AnaliticarController(EdunovaContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Analiticari.ToList());

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
                var analiticar = _context.Analiticari.Find(sifra);
                if (analiticar == null)
                {
                    return NotFound();
                }
                return Ok(analiticar);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]

        public IActionResult Post(Analiticar analiticar)
        {
            try
            {
                _context.Analiticari.Add(analiticar);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, analiticar);


            }

            catch (Exception e)
            {
                return BadRequest(e);



            }



        }


        [HttpPut("{sifra:int}")]
        public IActionResult Put(int sifra, Analiticar analiticar)
        {
            if (sifra < 1)
            {
                return BadRequest(new { poruka = "Šifra mora biti veæa od 0" });
            }

            try
            {
                Analiticar a = _context.Analiticari.Find(sifra);
                if (a == null)
                {
                    return NotFound();
                }

                a.Ime = analiticar.Ime;
                a.Prezime = analiticar.Prezime;
                a.Kontakt = analiticar.Kontakt;
                a.StrucnaSprema = analiticar.StrucnaSprema;

                _context.Analiticari.Update(a);
                _context.SaveChanges();
                return Ok(a);
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            if (sifra < 1)
            {
                return BadRequest(new { poruka = "Šifra mora biti veæa od 0" });
            }

            try
            {
                Analiticar a = _context.Analiticari.Find(sifra);
                if (a == null)
                {
                    return NotFound(new { poruka = "Analitièar ne postoji" });
                }

                _context.Analiticari.Remove(a);
                _context.SaveChanges();

                return Ok(new { poruka = "Analitièar je uspješno obrisan." });
            }
            catch (DbUpdateException dbEx)
            {
                // Ovo hvata grešku zbog vanjskog kljuèa
                return BadRequest(new
                {
                    poruka = "Ne možete obrisati ovog analitièara jer je povezan s drugim podacima."
                });
            }
            catch (Exception ex)
            {
                // Ostale neoèekivane greške
                return BadRequest(new { poruka = $"Dogodila se neoèekivana greška: {ex.Message}" });
            }
        }

    }
}
    

