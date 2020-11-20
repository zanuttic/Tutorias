using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Club_Proyect.Entity;

namespace Club_Proyect.Entities
{
    public class horario_Deporte
    {
        //public horario_Deporte() { socios = new List<Socio>(); }
        [Key]
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int cantidad_Socios { get; set; }
        public bool Activo { get; set; }
        public Deporte deporte { get; set; }

        public List<Socio> socios { get; set; }
        public Socio socio { get; set; }
    }
}
