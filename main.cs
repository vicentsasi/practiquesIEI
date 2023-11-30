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
            //string arxivo = XmlWrapper.ConvertXmlToJson("CAT.xml");
            //CATextractor.LoadJsonDataIntoDatabase(arxivo);

            string arxivo1 = CsvWrapper.ConvertCsvToJson("C:\\Users\\vsabsim.IIE-ASME16\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CV.csv");
            CVextractor.LoadJsonDataIntoDatabase(arxivo1);

            //string arxivo2 = JsonWrapper.ConvertToJson("C:\\Users\\vsabsim.IIE-ASME16\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
            //MURextractor.LoadJsonDataIntoDatabase(arxivo2);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
