using OpenQA.Selenium;
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
           
            ResCarga.Text = "";

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // Verifica si el nombre del elemento coincide
                if (itemChecked.ToString() == "Seleccionar todas")
                {
                    /*string directorioAplicacionMUR = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativa = Path.Combine(directorioAplicacionMUR,  "MUR.json");
                    string rutaAbsoluta = Path.GetFullPath(rutaRelativa);*/
                    string archivoJson = JsonWrapper.ConvertToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
                    logs = MURextractor.LoadJsonDataIntoDatabase(archivoJson, logs).ToString();

                    /*string directorioAplicacionCV = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaCsv = Path.Combine(directorioAplicacionCV, "CV.csv");
                    string rutaAbsolutaCsv = Path.GetFullPath(rutaRelativaCsv);*/
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CV.csv");
                    logs = CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV, logs).ToString();


                    /*string directorioAplicacionCAT = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaXml = Path.Combine(directorioAplicacionCAT, "CAT.xml");
                    string rutaAbsolutaXml = Path.GetFullPath(rutaRelativaXml);*/
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CAT.xml");
                    logs = CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT, logs).ToString();


                }
                else if (itemChecked.ToString() == "Murcia") {
                    string archivoJson = JsonWrapper.ConvertToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
                    logs =MURextractor.LoadJsonDataIntoDatabase(archivoJson, logs).ToString();
                }
                else if (itemChecked.ToString() == "Comunitat Valenciana")
                {
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CV.csv");
                    logs = CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV, logs).ToString();
                }
                else if (itemChecked.ToString() == "Catalunya")
                {
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CAT.xml");
                    logs = CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT, logs).ToString();
                }
            }
            ResCarga.Text = logs; 
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Principal().Show();
        }

        private void CargarRes(object sender, EventArgs e)
        {
            
        }
    }
}
