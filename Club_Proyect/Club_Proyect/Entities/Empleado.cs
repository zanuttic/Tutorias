using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Club_Proyect.Entity
{
    public class Empleado : Persona
    {
        [Key]
        public int Num_Legajo { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public string Sector { get; set; }
        public bool Activo_oNo { get; set; }
    }
}
