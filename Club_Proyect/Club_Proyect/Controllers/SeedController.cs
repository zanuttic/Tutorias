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
            var persona1 = new Persona()
            {
                ID = Guid.NewGuid(),    
                Nombre = "ivan",
                Apellido = "kopech",
                Direccion = "su casa",
                DNI = 42327447,
                FechaNacimiento = DateTime.Now,
                Activo = true,
            };
            var persona2 = new Persona()
            {
                ID = Guid.NewGuid(),
                Nombre = "mati",
                Apellido = "kopech",
                Direccion = "su casa",
                DNI = 42327555,
                FechaNacimiento = DateTime.Now,
                Activo = true,
            };
            _context.Persona.Add(persona1);
            _context.Persona.Add(persona2);

            var socio1 = new Socio()
            {
                ID = Guid.NewGuid(),
                persona = persona1,
                NumSocio = 1,
                FechaIngresoClub = DateTime.Now,
                Categoria = 1,
                ActivoOno = true,

            };
            var socio2 = new Socio()
            {
                ID = Guid.NewGuid(),
                persona = persona2,
                NumSocio = 2,
                FechaIngresoClub = DateTime.Now,
                Categoria = 1,
                ActivoOno = true,

            };
            _context.Socio.Add(socio1);
            _context.Socio.Add(socio2);

            await _context.SaveChangesAsync();

            
            return null;
        }

        
    }
}
