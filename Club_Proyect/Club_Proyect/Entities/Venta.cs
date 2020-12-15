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
        public Venta() { productos = new List<Productos>(); }
        [Key]
        public Guid ID { get; set; }

        public DateTime Fecha { get; set; }
        
        public List<Productos> productos;

        public string metodoPago { get; set; }
        public double Renta { get; set; }

        public double totalVenta { get; set; }

        [NotMapped]
        public string Prod { get; set; }


    }
}
