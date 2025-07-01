using BACKEND.Data;
using Microsoft.AspNetCore.Mvc;

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




    }
}
