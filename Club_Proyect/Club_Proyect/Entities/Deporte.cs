using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entities
{
    public class Deporte
    {
        [Key]
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        [NotMapped]
        public bool error { get; set; }

        [NotMapped]
        public string mensaje { get; set; }
    }
}
