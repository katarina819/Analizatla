using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdunovaApp.Models.DTO;

namespace BACKEND.Controller
{
    /// <summary>
    /// API kontroler za rad s uzorcima tla.
    /// Omoguæuje dohvat, unos, izmjenu i brisanje uzoraka te dohvat podataka za grafove.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UzorcitlaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        /// <summary>
        /// Konstruktor kontrolera koji prima DbContext.
        /// </summary>
        /// <param name="context">Instanca <see cref="EdunovaContext"/> za pristup bazi podataka.</param>
        public UzorcitlaController(EdunovaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaæa podatke za graf prikaza uzoraka tla.
        /// </summary>
        /// <returns>Lista DTO objekata <see cref="GrafUzorcitlaDTO"/> s nazivom lokacije, datumom uzorka i datumom prve analize.</returns>
        [HttpGet("graf")]
        public async Task<ActionResult<IEnumerable<GrafUzorcitlaDTO>>> GetGrafPodaci()
        {
                        var podaci = await _context.UzorciTla
                .Include(u => u.Lokacija)
                .Include(u => u.Analize)
                .Select(u => new GrafUzorcitlaDTO(
                    u.Lokacija.MjestoUzorkovanja,                     // naziv lokacije
                    u.Datum,                              // datum uzorka
                    u.Analize.OrderBy(a => a.Datum).FirstOrDefault().Datum   // prvi datum analize, nullable
                ))
                .ToListAsync();


            return Ok(podaci);
        }


        /// <summary>
        /// Dohvaæa sve uzorke tla.
        /// </summary>
        /// <returns>Lista DTO objekata <see cref="UzorcitlaDto"/> s podacima o uzorcima.</returns>
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

        /// <summary>
        /// Dohvaæa jedan uzorak tla po šifri.
        /// </summary>
        /// <param name="sifra">Šifra uzorka.</param>
        /// <returns>DTO objekt <see cref="UzorcitlaDto"/> ili NotFound ako uzorak ne postoji.</returns>
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

        /// <summary>
        /// Dodaje novi uzorak tla u bazu.
        /// </summary>
        /// <param name="dto">DTO objekt <see cref="UzorcitlaDto"/> s podacima o uzorku.</param>
        /// <returns>CreatedAtAction s podacima unesenog uzorka ili BadRequest ako postoji greška.</returns>
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

        /// <summary>
        /// Ažurira postojeæi uzorak tla.
        /// </summary>
        /// <param name="sifra">Šifra uzorka koji se ažurira.</param>
        /// <param name="dto">DTO objekt <see cref="UzorcitlaDto"/> s novim podacima.</param>
        /// <returns>NoContent ako je uspješno, NotFound ili BadRequest u sluèaju greške.</returns>
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

        /// <summary>
        /// Briše uzorak tla iz baze.
        /// </summary>
        /// <param name="sifra">Šifra uzorka koji se briše.</param>
        /// <returns>Ok poruka ako je uspješno, BadRequest ili NotFound u sluèaju greške.</returns>
        [HttpDelete("{sifra:int}")]
        public async Task<IActionResult> Delete(int sifra)
        {
            if (sifra <= 0)
                return BadRequest(new { poruka = "Šifra nije dobra" });

            try
            {
                var uzorak = await _context.UzorciTla.FindAsync(sifra);
                if (uzorak == null)
                    return NotFound(new { poruka = "Uzorak nije pronaðen" });

                _context.UzorciTla.Remove(uzorak);
                await _context.SaveChangesAsync();

                // Ako brisanje uspije
                return Ok(new { poruka = "Uzorak je uspješno obrisan." });
            }
            catch (DbUpdateException dbEx)
            {
                // Ako postoji foreign key constraint koji sprjeèava brisanje
                return BadRequest(new
                {
                    poruka = "Ne možete obrisati ovaj uzorak jer je povezan s drugim podacima.",
                    detalji = dbEx.InnerException?.Message
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
