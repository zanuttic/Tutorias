using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Club_Proyect.Entity
{
    public class Cliente : Persona
    {
        [Key]
        public int Num_Cliente { get; set; }
        public double Saldo { get; set; }
        public bool Activo_oNo { get; set; }
    }
}
