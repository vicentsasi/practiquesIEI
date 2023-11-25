using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiquesIEI.Entities
{
    public class centro_educativo
    {
        public string nombre { get; set; }
        public tipo_centro tipo { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }
        public int telefono { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public int cod_postal { get; set; }

        public centro_educativo() { }
        public centro_educativo(string nombre, tipo_centro tipo, double longitud, double latitud, int telefono, string descripcion, string direccion, int cod_postal)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.longitud = longitud;
            this.latitud = latitud;
            this.telefono = telefono;
            this.descripcion = descripcion;
            this.direccion = direccion;
            this.cod_postal = cod_postal;
        }
    }
}
