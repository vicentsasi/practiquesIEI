using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using practiquesIEI.Entities;
using practiquesIEI.Extractors;
using practiquesIEI;
using System.Diagnostics;




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
            //await ConexionBD.Conectar();
            string file = "C:\\Users\\Usuario\\source\\repos\\practiquesIEI\\Fuentes de datos\\murcia.json";
            string arxivo = JsonWrapper.ConvertToJson(file);
            MURextractor.LoadJsonDataIntoDatabase(arxivo);
            //centro_educativo centroPr = new centro_educativo("sant francesc de borja", tipo_centro.Público, 2023049, 2342834, 678363453, "es una escola de llombai molt re asea", "C/Avinguda Pais Valencia", 46195);
            //ConexionBD.insertCentro(centroPr);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
