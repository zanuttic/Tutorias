using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entity
{
    public class Persona 
    {
        [Key]
        public Guid ID { get; set; }
        public int DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public bool Activo { get; set; }



    }
}
