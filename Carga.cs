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

        private async void button2_Click(object sender, EventArgs e)
        {
            ResCarga.Text = "";
            MURextractor.inserts = 0;
            CATextractor.inserts = 0;
            CVextractor.inserts = 0;
            //CVextractor.inserts = 0;
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // Verifica si el nombre del elemento coincide
                if (itemChecked.ToString() == "Seleccionar todas")
                {
                    /*string directorioAplicacionMUR = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativa = Path.Combine(directorioAplicacionMUR,  "MUR.json");
                    string rutaAbsoluta = Path.GetFullPath(rutaRelativa);*/
                    string archivoJson = JsonWrapper.ConvertToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
                    await MURextractor.LoadJsonDataIntoDatabase(archivoJson);

                    /*string directorioAplicacionCV = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaCsv = Path.Combine(directorioAplicacionCV, "CV.csv");
                    string rutaAbsolutaCsv = Path.GetFullPath(rutaRelativaCsv);*/
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CV.csv");
                    await CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV);


                    /*string directorioAplicacionCAT = AppDomain.CurrentDomain.BaseDirectory;
                    string rutaRelativaXml = Path.Combine(directorioAplicacionCAT, "CAT.xml");
                    string rutaAbsolutaXml = Path.GetFullPath(rutaRelativaXml);*/
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CAT.xml");
                    await CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT);


                }
                else if (itemChecked.ToString() == "Murcia") {
                    string archivoJson = JsonWrapper.ConvertToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\MUR.json");
                    await MURextractor.LoadJsonDataIntoDatabase(archivoJson);
                }
                else if (itemChecked.ToString() == "Comunitat Valenciana")
                {
                    string archivoJsonCV = CsvWrapper.ConvertCsvToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CV.csv");
                    await CVextractor.LoadJsonDataIntoDatabase(archivoJsonCV);
                }
                else if (itemChecked.ToString() == "Catalunya")
                {
                    string archivoJsonCAT = XmlWrapper.ConvertXmlToJson("C:\\Users\\Sergi\\Source\\Repos\\vicentsasi\\practiquesIEI\\Fuentes de datos\\CAT.xml");
                    await CATextractor.LoadJsonDataIntoDatabase(archivoJsonCAT);
                }
            }
            ResCarga.Text = $"Número de registros cargados correctamente:{CATextractor.inserts + CVextractor.inserts + MURextractor.inserts}\r\n\r\n" +
                $"Registros con errores y reparados:\r\n{CATextractor.reparados}{MURextractor.reparados}{CVextractor.reparados}\r\n\r\n" +
                $"Registros con errores y rechazados:\r\n{CATextractor.eliminados}{MURextractor.eliminados}{CVextractor.eliminados}\r\n"; 
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Principal().Show();
        }

        private void CargarRes(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox checkBoxList = (CheckedListBox)sender;
            //si se marca "Seleccionar todos" desmarcar los otros items
            if (e.Index == 0)
            {
                for (int i = 0; i < checkBoxList.Items.Count; i++)
                {
                    if (i != e.Index)  
                    {
                        checkBoxList.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }
            //si se marca el item de catalunya, comunitat valenciana o murcia, desmarcar el de seleccionar todos
            if (e.Index != 0)
            {
                checkBoxList.SetItemCheckState(0, CheckState.Unchecked);
                   
            }

        }
    }
}
