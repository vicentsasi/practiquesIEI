using Newtonsoft.Json;
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
using System.Net.Http;
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
            ExtractionResult extractionResultCV = new ExtractionResult();
            ExtractionResult extractionResultCat = new ExtractionResult();
            ExtractionResult extractionResultMur = new ExtractionResult();
            //CVextractor.inserts = 0;
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // Verifica si el nombre del elemento coincide
                if (itemChecked.ToString() == "Seleccionar todas")
                {
                    
                    extractionResultMur = await cargarMur();
                    extractionResultCV = await cargarCV();
                    extractionResultCat = await cargarCat();

                }
                else if (itemChecked.ToString() == "Murcia") {
                    extractionResultMur = await cargarMur();
                }
                else if (itemChecked.ToString() == "Comunitat Valenciana")
                {
                    extractionResultCV = await cargarCV();
                }
                else if (itemChecked.ToString() == "Catalunya")
                {
                   extractionResultCat = await cargarCat();
                }
            }
            ResCarga.Text = $"Número de registros cargados correctamente:{extractionResultCat.Inserts + extractionResultCV.Inserts + extractionResultMur.Inserts}\r\n\r\n" +
                $"Registros con errores y reparados:\r\n{extractionResultCat.Reparados}{extractionResultMur.Reparados}{extractionResultCV.Reparados}\r\n\r\n" +
                $"Registros con errores y rechazados:\r\n{extractionResultCat.Eliminados}{extractionResultMur.Eliminados}{extractionResultCV.Eliminados}\r\n"; 
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Principal().Show();
        }

        private void CargarRes(object sender, EventArgs e)
        {
            
        }

        async Task<ExtractionResult> cargarMur() {
            ExtractionResult extractionResultMur = new ExtractionResult();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Reemplaza la URL con la dirección correcta de tu API
                    string apiUrl = "https://localhost:7194/api/Extractor/mur";

                    // Realiza la llamada a la API
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);

                    // Verifica si la llamada fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserializa el contenido a un objeto ExtractionResult
                        extractionResultMur = JsonConvert.DeserializeObject<ExtractionResult>(responseContent);

                        // Ahora puedes acceder a las propiedades de extractionResult
                        Console.WriteLine($"Eliminados: {extractionResultMur.Eliminados}");
                        Console.WriteLine($"Reparados: {extractionResultMur.Reparados}");
                        Console.WriteLine($"Inserts: {extractionResultMur.Inserts}");
                    }
                    else
                    {
                        Console.WriteLine($"Error en la llamada a la API. Código de estado: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return extractionResultMur;
        }

        async Task<ExtractionResult> cargarCat() {
            ExtractionResult extractionResultCat = new ExtractionResult();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Reemplaza la URL con la dirección correcta de tu API
                    string apiUrl = "https://localhost:7194/api/Extractor/cat";

                    // Realiza la llamada a la API
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);

                    // Verifica si la llamada fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserializa el contenido a un objeto ExtractionResult
                        extractionResultCat = JsonConvert.DeserializeObject<ExtractionResult>(responseContent);

                        // Ahora puedes acceder a las propiedades de extractionResult
                        Console.WriteLine($"Eliminados: {extractionResultCat.Eliminados}");
                        Console.WriteLine($"Reparados: {extractionResultCat.Reparados}");
                        Console.WriteLine($"Inserts: {extractionResultCat.Inserts}");
                    }
                    else
                    {
                        Console.WriteLine($"Error en la llamada a la API. Código de estado: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return extractionResultCat;
        }

        async Task<ExtractionResult> cargarCV() {
            ExtractionResult extractionResultCV = new ExtractionResult();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Reemplaza la URL con la dirección correcta de tu API
                    string apiUrl = "https://localhost:7194/api/Extractor/cv";

                    // Realiza la llamada a la API
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);

                    // Verifica si la llamada fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserializa el contenido a un objeto ExtractionResult
                        extractionResultCV = JsonConvert.DeserializeObject<ExtractionResult>(responseContent);

                        // Ahora puedes acceder a las propiedades de extractionResult
                        Console.WriteLine($"Eliminados: {extractionResultCV.Eliminados}");
                        Console.WriteLine($"Reparados: {extractionResultCV.Reparados}");
                        Console.WriteLine($"Inserts: {extractionResultCV.Inserts}");
                    }
                    else
                    {
                        Console.WriteLine($"Error en la llamada a la API. Código de estado: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return extractionResultCV;
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
