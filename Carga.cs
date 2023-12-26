using practiquesIEI.Extractors;
using practiquesIEI.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practiquesIEI
{
    public partial class Carga : Form
    {
        public Carga()
        {
            InitializeComponent();
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string logs = "";

            ConexionBD.BorrarCentros();

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // Verifica si el nombre del elemento coincide
                if (itemChecked.ToString() == "Seleccionar todas")
                {
                    string directorioAplicacionMUR = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativa = Path.Combine(directorioAplicacionMUR,  "MUR.json");
                    string rutaAbsoluta = Path.GetFullPath(rutaRelativa);
                    string archivoJson = JsonWrapper.ConvertToJson(rutaAbsoluta);
                    MURextractor.LoadJsonDataIntoDatabase(archivoJson, logs);

                    string directorioAplicacionCV = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaCsv = Path.Combine(directorioAplicacionCV, "CV.csv");
                    string rutaAbsolutaCsv = Path.GetFullPath(rutaRelativaCsv);
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson(rutaAbsolutaCsv);
                    CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV, logs);


                    string directorioAplicacionCAT = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaXml = Path.Combine(directorioAplicacionCAT, "CAT.xml");
                    string rutaAbsolutaXml = Path.GetFullPath(rutaRelativaXml);
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson(rutaAbsolutaXml);
                    CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT, logs);


                }
                else if (itemChecked.ToString() == "Murcia") {

                    string directorioAplicacionMUR = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativa = Path.Combine(directorioAplicacionMUR, "MUR.json");
                    string rutaAbsoluta = Path.GetFullPath(rutaRelativa);
                    string archivoJson = JsonWrapper.ConvertToJson(rutaAbsoluta);
                    MURextractor.LoadJsonDataIntoDatabase(archivoJson, logs);
                }
                else if (itemChecked.ToString() == "Comunitat Valenciana")
                {
                    string directorioAplicacionCV = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaCsv = Path.Combine(directorioAplicacionCV, "CV.csv");
                    string rutaAbsolutaCsv = Path.GetFullPath(rutaRelativaCsv);
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson(rutaAbsolutaCsv);
                    CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV, logs);
                }
                else if (itemChecked.ToString() == "Catalunya")
                {
                    string directorioAplicacionCAT = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaXml = Path.Combine(directorioAplicacionCAT,  "CAT.xml");
                    string rutaAbsolutaXml = Path.GetFullPath(rutaRelativaXml);
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson(rutaAbsolutaXml);
                    CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT, logs);
                }
            }

            tbLogs.Text = logs.ToString();

            

            
        }
    }
}
