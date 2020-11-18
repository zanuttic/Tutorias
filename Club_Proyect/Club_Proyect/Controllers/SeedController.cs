using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Data;
using Club_Proyect.Entity;
using Microsoft.AspNetCore.Identity;

namespace Club_Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SeedController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/Seed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersona()
        {
            //crear 1 instancia de Rol == > Admin
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            //crear una instancia de User
            //instancia objeto User
            var user = new IdentityUser { UserName = "Admin@gmail.com", Email = "Admin@gmail.com" };
            //crea la instancia en base de datos de User con Password
            await _userManager.CreateAsync(user, "Password123");

            //asignarle al usuario el rol Admin
            await _userManager.AddToRoleAsync(user, "Admin");

            //crear 02 instancia de persona
            //crear 02 instancia de Socio con la persona creada

            //return await _context.Persona.ToListAsync();
            return null;
        }

        
    }
}
