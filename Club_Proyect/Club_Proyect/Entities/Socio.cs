using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entity
{
    public class Socio 
    {
        public Persona persona { get; set; }
        [Key]
        public Guid ID { get; set; }
        public int NumSocio { get; set; }

        public DateTime FechaIngresoClub { get; set; }

        public int Categoria { get; set; }

        public bool ActivoOno { get; set; }


    }
}
