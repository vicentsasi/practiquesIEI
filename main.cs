using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using practiquesIEI.Entities;
using practiquesIEI.Extractors;
using practiquesIEI;
using System.Diagnostics;
using practiquesIEI.Wrappers;

namespace practiquesIEI
{
    public static class main
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

             ConexionBD.Conectar();
            ConexionBD.BorrarCentros();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Búsqueda());
        }
    }
}
