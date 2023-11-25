using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiquesIEI.Entities
{
    public class provincia
    {
        public int codigo { get; set; }
        public string nombre { get; set; }

        public provincia() { }
        public provincia(int codigo, string nombre) { 
            this.codigo = codigo;
            this.nombre = nombre;
        }
    }
}
