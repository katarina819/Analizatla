using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BACKEND.Controllers
{
    /// <summary>
    /// Kontroler za autentikaciju i autorizaciju operatera.
    /// Omoguæuje generiranje JWT tokena na temelju vjerodajnica.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutorizacijaController : ControllerBase
    {
        private readonly EdunovaContext _context;

        /// <summary>
        /// Konstruktor kontrolera.
        /// </summary>
        /// <param name="context">Instanca <see cref="EdunovaContext"/> za pristup bazi podataka.</param>
        public AutorizacijaController(EdunovaContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Generira JWT token za prijavljenog operatera.
        /// </summary>
        /// <param name="operater">DTO objekt <see cref="OperaterDTO"/> koji sadrži email i lozinku operatera.</param>
        /// <returns>JWT token kao string ako su vjerodajnice ispravne, 403 Forbidden ako nije autoriziran, 500 InternalServerError u sluèaju problema s bazom.</returns>
        [HttpPost("token")]
        public IActionResult GenerirajToken(OperaterDTO operater)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // TEST: provjera može li EF dohvatiti tablicu
            try
            {
                var test = _context.Operateri
                    .FromSqlRaw("SELECT TOP 1 * FROM dbo.operateri")
                    .FirstOrDefault();

                if (test == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Tablica je prazna ili nedostupna.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška pri dohvaæanju tablice: {ex.Message}");
            }

            // Normalni dohvat po emailu
            var operBaza = _context.Operateri
                .Where(p => p.Email!.Equals(operater.Email))
                .FirstOrDefault();

            if (operBaza == null)
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, ne mogu naæi operatera");

            Console.WriteLine("Unesena lozinka: " + operater.Password);
            Console.WriteLine("Hash iz baze: " + operBaza.Lozinka);
            Console.WriteLine("Verify: " + BCrypt.Net.BCrypt.Verify(operater.Password, operBaza.Lozinka));

            if (!BCrypt.Net.BCrypt.Verify(operater.Password, operBaza.Lozinka))
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, lozinka ne odgovara");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("MojKljucKojijeJakoTajan i dovoljno dugaèak da se može koristiti");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);


        }

    }
}