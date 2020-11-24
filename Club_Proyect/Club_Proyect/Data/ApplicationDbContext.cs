using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Entities;
using Club_Proyect.Entity;

namespace Club_Proyect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }
        public DbSet<Club_Proyect.Entity.Persona> Persona { get; set; }

        public DbSet<Club_Proyect.Entity.Cliente> Cliente { get; set; }

        public DbSet<Club_Proyect.Entity.Empleado> Empleado { get; set; }

        public DbSet<Club_Proyect.Entity.Socio> Socio { get; set; }

        public DbSet<Club_Proyect.Entities.Vecino> Vecino { get; set; }
        public DbSet<Club_Proyect.Entities.Deporte> Deporte { get; set; }
        public DbSet<Club_Proyect.Entities.horario_Deporte> horario_Deporte { get; set; }

    }
}
