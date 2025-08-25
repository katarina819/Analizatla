using AutoMapper;
using BACKEND.Data;
using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EdunovaAPP.Controllers
{
    /// <summary>
    /// Apstraktni bazni kontroler za Edunova aplikaciju.
    /// Omogu�uje zajedni�ke funkcionalnosti i pristup kontekstu baze podataka te AutoMapperu.
    /// Naslje�ivanje ovog kontrolera omogu�uje kori�tenje Dependency Injectiona za <see cref="EdunovaContext"/> i <see cref="IMapper"/>.
    /// </summary>
    [Authorize]
    public abstract class EdunovaController : ControllerBase
    {
        /// <summary>
        /// Instanca <see cref="EdunovaContext"/> za pristup bazi podataka.
        /// </summary>

        // dependecy injection
        // 1. definira� privatno svojstvo
        protected readonly EdunovaContext _context;

        /// <summary>
        /// Instanca <see cref="IMapper"/> za mapiranje DTO objekata.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Konstruktor baznog kontrolera.
        /// </summary>
        /// <param name="context">Instanca <see cref="EdunovaContext"/> koja se koristi za pristup bazi podataka.</param>
        /// <param name="mapper">Instanca <see cref="IMapper"/> koja se koristi za mapiranje entiteta i DTO objekata.</param>
        // dependecy injection
        // 2. proslijedi� instancu kroz konstruktor
        public EdunovaController(EdunovaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}