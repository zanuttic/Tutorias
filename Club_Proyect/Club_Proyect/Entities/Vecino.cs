using Club_Proyect.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club_Proyect.Entities
{
    public class Vecino
    {
        public Persona persona { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string telefono { get; set; }
    }
}
