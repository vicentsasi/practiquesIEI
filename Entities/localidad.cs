using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiquesIEI.Entities
{
    public class localidad
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string prov_nombre { get; set; }

        public localidad() { }
        public localidad(string codigo, string nombre, string prov_nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.prov_nombre = prov_nombre;
        }
    }
}
