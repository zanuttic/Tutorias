using Club_Proyect.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entities
{
    public class Bufet
    {
        [Key]
        public Guid ID { get; set; }
        public Cliente cliente { get; set; }
        
        public DateTime Horario { get; set; }

        public Venta venta { get; set; }
    }
}
