using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entities
{
    public class Productos
    {
        [Key]
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public double Precio  { get {
                var costoImpuesto = Costo + (Costo * Impuesto / 100);
                var precio = costoImpuesto + (costoImpuesto * Ganancia / 100);

                return  precio; } 
        }
        public int Stock { get; set; }
        public double Costo { get; set; }
        public double Impuesto { get; set; }
        public double Ganancia { get; set; }



    }
}
