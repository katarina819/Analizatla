using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BACKEND.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class LokacijaController:ControllerBase
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
                var lokacija= _context.Lokacije.Find(sifra);
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

        [HttpPut("sifra:int")]
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
        public IActionResult Delete(int sifra)
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


                _context.Lokacije.Remove(l);
                _context.SaveChanges();
                return NoContent();
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
