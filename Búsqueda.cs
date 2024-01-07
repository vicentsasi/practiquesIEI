using OpenQA.Selenium.DevTools;
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
        public Búsqueda()
        {
            InitializeComponent();
            LoadMap();
        }

        #region BOTONES
        private async void button1_Click(object sender, EventArgs e)
        {
            wbMapa.Document.InvokeScript("eval", new object[] { "removeAllMarkers();" });
            //tbLogs.Text = "";

            centros = await ConexionBD.getAllCentros();
            BindingList<object> bindinglist = new BindingList<object>();
            if (centros != null)
            {
                foreach (centro_educativo centro in centros)
                {
                    bindinglist.Add(new
                    {
                        nombre = centro.nombre,
                        tipo = centro.tipo,
                        direccion = centro.direccion,
                        loc = centro.loc_nombre,
                        prov = centro.prov_nombre,
                        desc = centro.descripcion,
                        cod_postal = centro.cod_postal
                    });
                    AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");

                }
            }
            bindingSource1.DataSource = bindinglist;
        }

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
            wbMapa.Document.InvokeScript("eval", new object[] { "removeAllMarkers();" });
            string localidad = tbLocalidad.Text;
            string tipo ="";
            if (cbTipo.SelectedIndex != -1) { tipo = cbTipo.SelectedItem.ToString(); }
            string provincia = tbProv.Text;
            string cod_postal = tbCP.Text;

            //obtiene los centros y los introduce en el dataGridView
            centros = await ConexionBD.FindCentros(localidad, tipo, provincia, cod_postal);
            BindingList<object> bindinglist = new BindingList<object>();
            if (centros != null)
            {
                foreach (centro_educativo centro in centros)
                {
                    bindinglist.Add(new
                    {
                        nombre = centro.nombre,
                        tipo = centro.tipo,
                        direccion = centro.direccion,
                        loc = centro.loc_nombre,
                        prov = centro.prov_nombre,
                        desc = centro.descripcion,
                        cod_postal = centro.cod_postal
                    });
                    AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");

                }
            }
            bindingSource1.DataSource = bindinglist;

            /*if (localidad != "" && tipo == "" && provincia == "" && cod_postal == "") {
                centros = await ConexionBD.FindCentrosByLocalidad(localidad);
                if (centros != null)
                {
                    foreach (var centro in centros)
                    {
                        //tbLogs.Lines = tbLogs.Lines.Append(centro.nombre).ToArray();
                        AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");
                    }
                }
                else { 
                    //tbLogs.Text = "No se han encontrado resultados."; 
                }
            }
            if (localidad == "" && tipo != "" && provincia == "" && cod_postal == "")
            {
                centros = await ConexionBD.FindCentrosByTipo(tipo);
                if (centros != null)
                {
                    foreach (var centro in centros)
                    {
                        //tbLogs.Lines = tbLogs.Lines.Append(centro.nombre).ToArray();
                        AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");
                    }
                }
                else { 
                    //tbLogs.Text = "No se han encontrado resultados."; 
                }
            }
            if (localidad == "" && tipo == "" && provincia != "" && cod_postal == "")
            {
                centros = await ConexionBD.FindCentrosByProvincia(provincia);
                if (centros != null)
                {
                    foreach (var centro in centros)
                    {
                        //tbLogs.Lines = tbLogs.Lines.Append(centro.nombre).ToArray();
                        AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");
                    }
                }
                else { 
                    //tbLogs.Text = "No se han encontrado resultados.";
                }
            }
            if (localidad == "" && tipo == "" && provincia == "" && cod_postal != "")
            {
                centros = await ConexionBD.FindCentrosByCod_Postal(cod_postal);
                if (centros != null)
                {
                    foreach (var centro in centros)
                    {
                        //tbLogs.Lines = tbLogs.Lines.Append(centro.nombre).ToArray();
                        AddMarker(centro.latitud, centro.longitud, $"{centro.nombre}");
                    }
                }
                else { 
                    //tbLogs.Text = "No se han encontrado resultados."; 
                }
            }*/

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

                                    var markers = [];
                                    function addMarker(lat, lng, popupText) {
                                    var marker = L.marker([lat, lng]).addTo(map);
                                    marker.bindPopup(popupText);
                                    markers.push(marker);
                                    }

                                    function removeAllMarkers() {
                                        for (var i = 0; i < markers.length; i++) {
                                            map.removeLayer(markers[i]); // Elimina el marcador del mapa
                                        }
                                        markers = []; // Vacía el array de marcadores
                                    }


                                    
                                   
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
