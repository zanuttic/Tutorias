using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Entity;
using Club_Proyect.Entities;

namespace Club_Proyect.Data
{
    public class Club_ProyectContext : DbContext
    {
        public Club_ProyectContext (DbContextOptions<Club_ProyectContext> options)
            : base(options)
        {
        }

        public DbSet<Club_Proyect.Entity.Persona> Persona { get; set; }

        public DbSet<Club_Proyect.Entity.Cliente> Cliente { get; set; }

        public DbSet<Club_Proyect.Entity.Empleado> Empleado { get; set; }

        public DbSet<Club_Proyect.Entity.Socio> Socio { get; set; }

        public DbSet<Club_Proyect.Entities.Vecino> Vecino { get; set; }
    }
}
