using BACKEND.Data;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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




    }
}
