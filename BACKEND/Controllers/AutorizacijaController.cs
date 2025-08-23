using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BACKEND.Data;
using BACKEND.DTOs;

namespace BACKEND.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutorizacijaController(EdunovaContext context) : ControllerBase
    {

        private readonly EdunovaContext _context = context;


        [HttpPost("token")]
        public IActionResult GenerirajToken(OperaterDTO operater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operBaza = _context.Operateri
                   .Where(p => p.Email!.Equals(operater.Email))
                   .FirstOrDefault();

            if (operBaza == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, ne mogu na�i operatera");
            }

            if (!BCrypt.Net.BCrypt.Verify(operater.Password, operBaza.Lozinka))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, lozinka ne odgovara");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("MojKljucKojijeJakoTajan i dovoljno duga�ak da se mo�e koristiti");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}