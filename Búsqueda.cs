using practiquesIEI.Entities;
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
    public partial class Búsqueda : Form
    {

        List<centro_educativo> centros;
        Carga cargaPrin;
        public Búsqueda()
        {
            InitializeComponent();
            LoadMap();
        }

        #region BOTONES

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            // Llamada a la función para agregar marcadores
            //AddMarker("41.3851", "2.1734", "Barcelona, España");
            this.Hide();
            new Principal().Show();

        }

        private async void btAceptar_Click(object sender, EventArgs e)
        {
           
            tbLogs.Text = "";
            string localidad = tbLocalidad.Text;
            string tipo ="";
            if (cbTipo.SelectedIndex != -1) { tipo = cbTipo.SelectedItem.ToString(); }
            string provincia = tbProv.Text;
            string cod_postal = tbCP.Text;
         
            if (localidad != "" && tipo == "" && provincia == "" && cod_postal == "") {
                centros = await ConexionBD.FindCentrosByLocalidad(localidad);
                if (centros != null)
                {
                    foreach (var centro in centros)
                    {
                        Console.WriteLine(centro);
                        tbLogs.Text += $"{centro.nombre}\n";
                        AddMarker(centro.latitud, centro.longitud, centro.nombre);
                    }
                }
                else { tbLogs.Text = "No se han encontrado resultados."; }
            }
            

        }

        private void btCarga_Click(object sender, EventArgs e)
        {
            this.Close();
            cargaPrin.Show();
        }

        private void btCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            cargaPrin.Close();
        }

        #endregion
        private void LoadMap()
        {
            string html = @"<!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset=""utf-8"" />
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <link rel=""stylesheet"" href=""https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"" />
                                <script src=""https://unpkg.com/leaflet@1.7.1/dist/leaflet.js""></script>
                                <style>
                                    #map {
                                        width: 600px;
                                        height: 520px;
                                    }
                                </style>
                            </head>
                            <body>
                                <div id=""map""></div>
                                <script>
                                    var map = L.map('map').setView([40.5, -4], 6);

                                    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                                        attribution: 'Map data &copy; <a href=""http://openstreetmap.org"">OpenStreetMap</a> contributors, <a href=""http://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>, Imagery © <a href=""http://cloudmade.com"">CloudMade</a>',
                                        maxZoom: 18
                                    }).addTo(map);

                                    L.control.scale().addTo(map);

                                    function addMarker(lat, lng, popupText) {
                                    var marker = L.marker([lat, lng]).addTo(map);
                                    marker.bindPopup(popupText);
                                    }


                                    // Agrega un marcador en Valencia
                                    addMarker(39.4699,-0.3763,'Valencia, España')
                                </script>
                            </body>
                            </html>


                            ";

            wbMapa.DocumentText = html;
        }

        private void AddMarker(string lat, string lng, string popupText)
        {
            try
            {
                string script = $"addMarker({lat}, {lng}, '{popupText}');";
                wbMapa.Document.InvokeScript("eval", new object[] { script });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar marcador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
