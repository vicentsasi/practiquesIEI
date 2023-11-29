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
        static async Task Main()
        {   
            await ConexionBD.Conectar();
            await ConexionBD.BorrarCentros();
            string file = "C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\murcia.json";
            string arxivo = JsonWrapper.ConvertToJson(file);
            MURextractor.LoadJsonDataIntoDatabase(arxivo);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
