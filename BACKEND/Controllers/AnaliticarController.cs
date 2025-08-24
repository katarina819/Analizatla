using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using BACKEND.Models.DTO;
using BACKEND.Services;
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
        private readonly SlikaService _slikaService;


        public AnaliticarController(EdunovaContext context, SlikaService slikaService)
        {
            _context = context;
            _slikaService = slikaService;

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _context.Analiticari
                    .Select(a => new AnaliticarDTO
                    {
                        Sifra = a.Sifra,
                        Ime = a.Ime,
                        Prezime = a.Prezime,
                        Kontakt = a.Kontakt,
                        StrucnaSprema = a.StrucnaSprema,
                        SlikaUrl = a.SlikaUrl
                    })
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{sifra:int}")]
        public async Task<IActionResult> Get(int sifra)
        {
            if (sifra <= 0)
                return BadRequest("Šifra nije dobra");

            try
            {
                var analiticar = await _context.Analiticari
                    .Where(a => a.Sifra == sifra)
                    .Select(a => new AnaliticarDTO
                    {
                        Sifra = a.Sifra,
                        Ime = a.Ime,
                        Prezime = a.Prezime,
                        Kontakt = a.Kontakt,
                        StrucnaSprema = a.StrucnaSprema,
                        SlikaUrl = a.SlikaUrl
                    })
                    .FirstOrDefaultAsync();

                if (analiticar == null)
                    return NotFound();

                return Ok(analiticar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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


        [HttpPost("{sifra}/slika")]
        public async Task<IActionResult> DodajSliku(int sifra, [FromBody] SlikaDTO dto)
        {
            var analiticar = await _context.Analiticari.FindAsync(sifra);
            if (analiticar == null)
                return NotFound("Analitièar ne postoji");

            var fileName = $"{sifra}.jpg";

            // Ruèna instanca SlikaService
            var slikaService = new SlikaService(this.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>());
            var slikaUrl = slikaService.SpremiSliku(dto.Base64, fileName);

            analiticar.SlikaUrl = slikaUrl;

            await _context.SaveChangesAsync();

            return Ok(new { poruka = "Slika spremljena", slikaUrl });
        }


        // GET api/v1/Analiticar/trazi/{uvjet}
        [HttpGet("trazi/{uvjet}")]
        public async Task<IActionResult> TraziAnaliticara(string uvjet)
        {
            if (string.IsNullOrWhiteSpace(uvjet) || uvjet.Length < 3)
                return BadRequest("Uvjet mora imati barem 3 znaka");

            uvjet = uvjet.ToLower();

            try
            {
                var query = _context.Analiticari.AsQueryable();

                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(a => a.Ime.ToLower().Contains(s) || a.Prezime.ToLower().Contains(s));
                }

                var lista = await query
                    .Select(a => new AnaliticarDTO
                    {
                        Sifra = a.Sifra,
                        Ime = a.Ime,
                        Prezime = a.Prezime,
                        Kontakt = a.Kontakt,
                        StrucnaSprema = a.StrucnaSprema,
                        SlikaUrl = a.SlikaUrl
                    })
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
        }

        // GET api/v1/Analiticar/traziStranicenje/{stranica}?uvjet=...
        [HttpGet("traziStranicenje/{stranica}")]
        public async Task<IActionResult> TraziAnaliticaraStranicenje(int stranica, string uvjet = "")
        {
            int poStranici = 4;
            uvjet = uvjet.ToLower();

            try
            {
                var query = _context.Analiticari.AsQueryable();

                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(a => a.Ime.ToLower().Contains(s) || a.Prezime.ToLower().Contains(s));
                }

                var ukupno = await query.CountAsync(); // ukupno rezultata
                var ukupnoStranica = (int)Math.Ceiling((double)ukupno / poStranici);

                var lista = await query
                    .OrderBy(a => a.Prezime)
                    .Skip((poStranici * (stranica - 1)))
                    .Take(poStranici)
                    .Select(a => new AnaliticarDTO
                    {
                        Sifra = a.Sifra,
                        Ime = a.Ime,
                        Prezime = a.Prezime,
                        Kontakt = a.Kontakt,
                        StrucnaSprema = a.StrucnaSprema,
                        SlikaUrl = a.SlikaUrl
                    })
                    .ToListAsync();

                return Ok(new { lista, ukupnoStranica });
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
        }


    }
}
    

