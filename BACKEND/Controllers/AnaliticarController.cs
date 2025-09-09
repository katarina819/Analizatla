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
    /// <summary>
    /// Kontroler za upravljanje analiti�arima.
    /// Omogu�uje CRUD operacije, dodavanje slike, pretra�ivanje i paginaciju.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

    public class AnaliticarController : ControllerBase
    {
        private readonly EdunovaContext _context;
        private readonly SlikaService _slikaService;

        /// <summary>
        /// Konstruktor kontrolera.
        /// </summary>
        /// <param name="context">Instanca <see cref="EdunovaContext"/> za pristup bazi podataka.</param>
        /// <param name="slikaService">Instanca <see cref="SlikaService"/> za upravljanje slikama.</param>
        public AnaliticarController(EdunovaContext context, SlikaService slikaService)
        {
            _context = context;
            _slikaService = slikaService;

        }

        /// <summary>
        /// Dohva�a sve analiti�are.
        /// </summary>
        /// <returns>Lista svih analiti�ara u obliku <see cref="AnaliticarDTO"/>.</returns>
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

        /// <summary>
        /// Dohva�a analiti�ara po �ifri.
        /// </summary>
        /// <param name="sifra">Jedinstvena �ifra analiti�ara.</param>
        /// <returns>Objekt <see cref="AnaliticarDTO"/> ako postoji, 404 NotFound ina�e.</returns>
        [HttpGet("{sifra:int}")]
        public async Task<IActionResult> Get(int sifra)
        {
            if (sifra <= 0)
                return BadRequest("�ifra nije dobra");

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


        /// <summary>
        /// Dodaje novog analiti�ara u bazu.
        /// </summary>
        /// <param name="analiticar">Objekt <see cref="Analiticar"/> s podacima za kreiranje.</param>
        /// <returns>Stvoreni objekt <see cref="Analiticar"/> s HTTP status 201 Created.</returns>
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

        /// <summary>
        /// A�urira postoje�eg analiti�ara.
        /// </summary>
        /// <param name="sifra">�ifra analiti�ara koji se a�urira.</param>
        /// <param name="analiticar">Objekt <see cref="Analiticar"/> s novim podacima.</param>
        /// <returns>HTTP 200 OK ako je uspje�no a�urirano, 404 ako ne postoji.</returns>
        [HttpPut("{sifra:int}")]
        public IActionResult Put(int sifra, Analiticar analiticar)
        {
            if (sifra < 1)
            {
                return BadRequest(new { poruka = "�ifra mora biti ve�a od 0" });
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

        /// <summary>
        /// Bri�e analiti�ara po �ifri.
        /// </summary>
        /// <param name="sifra">�ifra analiti�ara koji se bri�e.</param>
        /// <returns>Poruka o uspjehu ili neuspjehu brisanja.</returns>
        [HttpDelete("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            if (sifra < 1)
            {
                return BadRequest(new { poruka = "�ifra mora biti ve�a od 0" });
            }

            try
            {
                Analiticar a = _context.Analiticari.Find(sifra);
                if (a == null)
                {
                    return NotFound(new { poruka = "Analiti�ar ne postoji" });
                }

                _context.Analiticari.Remove(a);
                _context.SaveChanges();

                return Ok(new { poruka = "Analiti�ar je uspje�no obrisan." });
            }
            catch (DbUpdateException dbEx)
            {
                // Ovo hvata gre�ku zbog vanjskog klju�a
                return BadRequest(new
                {
                    poruka = "Ne mo�ete obrisati ovog analiti�ara jer je povezan s drugim podacima."
                });
            }
            catch (Exception ex)
            {
                // Ostale neo�ekivane gre�ke
                return BadRequest(new { poruka = $"Dogodila se neo�ekivana gre�ka: {ex.Message}" });
            }
        }

        /// <summary>
        /// Dodaje sliku analiti�aru.
        /// </summary>
        /// <param name="sifra">�ifra analiti�ara.</param>
        /// <param name="dto">DTO objekt <see cref="SlikaDTO"/> s Base64 podacima slike.</param>
        /// <returns>Poruka i URL spremljene slike.</returns>
        [HttpPost("{sifra}/slika")]
        public async Task<IActionResult> DodajSliku(int sifra, [FromBody] SlikaDTO dto)
        {
            var analiticar = await _context.Analiticari.FindAsync(sifra);
            if (analiticar == null)
                return NotFound("Analiti�ar ne postoji");

            var fileName = $"{sifra}.jpg";

            // Ru�na instanca SlikaService
            var slikaService = new SlikaService(this.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>());
            var slikaUrl = slikaService.SpremiSliku(dto.Base64, fileName);

            analiticar.SlikaUrl = slikaUrl;

            await _context.SaveChangesAsync();

            return Ok(new { poruka = "Slika spremljena", slikaUrl });
        }

        /// <summary>
        /// Pretra�uje analiti�are po imenu ili prezimenu.
        /// </summary>
        /// <param name="uvjet">Uvjet za pretra�ivanje (barem 3 znaka).</param>
        /// <returns>Lista <see cref="AnaliticarDTO"/> koji zadovoljavaju uvjet pretra�ivanja.</returns>
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


        /// <summary>
        /// Pretra�uje analiti�are s podr�kom za paginaciju.
        /// </summary>
        /// <param name="stranica">Broj stranice koju �elimo dohvatiti.</param>
        /// <param name="uvjet">Opcionalni uvjet za pretra�ivanje.</param>
        /// <returns>Objekt s listom rezultata i ukupnim brojem stranica.</returns>
        // GET api/v1/Analiticar/traziStranicenje/{stranica}?uvjet=...
        [HttpGet("traziStranicenje/{stranica}")]
        public async Task<IActionResult> TraziAnaliticaraStranicenje(int stranica, string uvjet = "")
        {
            int poStranici = 4;

            try
            {
                var query = _context.Analiticari.AsQueryable();

                if (!string.IsNullOrWhiteSpace(uvjet) && uvjet.Length >= 3)
                {
                    foreach (var s in uvjet.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    {
                        string pattern = $"%{s}%";
                        query = query.Where(a =>
                            EF.Functions.ILike(a.Ime, pattern) ||
                            EF.Functions.ILike(a.Prezime, pattern));
                    }
                }

                var ukupno = await query.CountAsync();
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
    

