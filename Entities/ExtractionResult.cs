using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiquesIEI
{
    internal class ExtractionResult
    {
        public string Eliminados { get; set; }
        public string Reparados { get; set; }
        public int Inserts { get; set; }
        public string ErrorMessage { get; set; }
    }
}
