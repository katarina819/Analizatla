using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UzorcitlaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        public UzorcitlaController(EdunovaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var uzorci = await _context.UzorciTla
                    .Include(u => u.Lokacija)
                    .Select(u => new UzorcitlaDto
                    {
                        Sifra = u.Sifra,
                        MasaUzorka = u.MasaUzorka,
                        VrstaTla = u.VrstaTla,
                        Datum = u.Datum,
                        MjestoUzorkovanja = u.Lokacija.MjestoUzorkovanja
                    })
                    .ToListAsync();

                return Ok(uzorci);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{sifra:int}")]
        public async Task<IActionResult> Get(int sifra)
        {
            if (sifra <= 0) return BadRequest("Šifra nije dobra");

            try
            {
                var uzorak = await _context.UzorciTla
                    .Include(u => u.Lokacija)
                    .Where(u => u.Sifra == sifra)
                    .Select(u => new UzorcitlaDto
                    {
                        Sifra = u.Sifra,
                        MasaUzorka = u.MasaUzorka,
                        VrstaTla = u.VrstaTla,
                        Datum = u.Datum,
                        MjestoUzorkovanja = u.Lokacija.MjestoUzorkovanja
                    })
                    .FirstOrDefaultAsync();

                if (uzorak == null) return NotFound("Uzorak nije pronaðen");

                return Ok(uzorak);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UzorcitlaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var lokacija = await _context.Lokacije
                    .FirstOrDefaultAsync(l => l.MjestoUzorkovanja == dto.MjestoUzorkovanja);

                if (lokacija == null)
                    return BadRequest("Nepoznata lokacija");

                var uzorak = new Uzorcitla
                {
                    MasaUzorka = dto.MasaUzorka,
                    VrstaTla = dto.VrstaTla,
                    Datum = dto.Datum,
                    LokacijaId = lokacija.Sifra
                };

                _context.UzorciTla.Add(uzorak);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { sifra = uzorak.Sifra }, uzorak);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{sifra:int}")]
        public async Task<IActionResult> Put(int sifra, [FromBody] UzorcitlaDto dto)
        {
            if (sifra <= 0) return BadRequest("Šifra nije dobra");

            try
            {
                var uzorak = await _context.UzorciTla.FindAsync(sifra);
                if (uzorak == null) return NotFound("Uzorak nije pronaðen");

                var lokacija = await _context.Lokacije
                    .FirstOrDefaultAsync(l => l.MjestoUzorkovanja == dto.MjestoUzorkovanja);

                if (lokacija == null)
                    return BadRequest("Nepoznata lokacija");

                uzorak.MasaUzorka = dto.MasaUzorka;
                uzorak.VrstaTla = dto.VrstaTla;
                uzorak.Datum = dto.Datum;
                uzorak.LokacijaId = lokacija.Sifra;

                _context.UzorciTla.Update(uzorak);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{sifra:int}")]
        public async Task<IActionResult> Delete(int sifra)
        {
            if (sifra <= 0) return BadRequest("Šifra nije dobra");

            try
            {
                var uzorak = await _context.UzorciTla.FindAsync(sifra);
                if (uzorak == null) return NotFound("Uzorak nije pronaðen");

                _context.UzorciTla.Remove(uzorak);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
