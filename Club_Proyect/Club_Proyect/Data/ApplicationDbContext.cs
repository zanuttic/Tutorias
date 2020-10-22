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

        //public DbSet<Persona> personas = new DbSet<Perso
        // public DbSet<Cliente> clientes { get; set; }
        // public DbSet<Empleado> empleados { get; set; }
        // public DbSet<Socio> socios { get; set; }
        // public DbSet<Vecino> vecinos { get; set; }

    }
}
