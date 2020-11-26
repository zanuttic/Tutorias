using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_Proyect.Entity
{
    public class Cliente
    {
        public Persona persona {get;set;}

        [Key]
        public Guid ID { get; set; }
        public int Num_Cliente { get; set; }
        public double Saldo { get; set; }
        public bool Activo_oNo { get; set; }
        [NotMapped]
        public bool error { get; set; }

        [NotMapped]
        public string mensaje { get; set; }
    }
}
