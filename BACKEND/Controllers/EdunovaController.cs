using AutoMapper;
using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EdunovaAPP.Controllers
{
    [Authorize]
    public abstract class EdunovaController : ControllerBase
    {

        // dependecy injection
        // 1. definira� privatno svojstvo
        protected readonly EdunovaContext _context;

        protected readonly IMapper _mapper;


        // dependecy injection
        // 2. proslijedi� instancu kroz konstruktor
        public EdunovaController(EdunovaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}