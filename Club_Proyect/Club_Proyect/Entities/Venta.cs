using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entities
{
    public class Venta
    {
        
        [Key]
        public Guid ID { get; set; }

        public DateTime Fecha { get; set; }
        
        public Productos productos { get; set; }

        public string metodoPago { get; set; }
        public double Renta { get; set; }

        public double totalVenta { get; set; }

        [NotMapped]
        public string Prod { get; set; }

        public bool ActivooNo { get; set; }


    }
}
