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
using OpenQA.Selenium;

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
            string archivoJson = JsonWrapper.ConvertToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
            string logs = "";
            MURextractor.LoadJsonDataIntoDatabase(archivoJson, logs);
           
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Carga());*/
        }
    }
}
