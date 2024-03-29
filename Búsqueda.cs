﻿using Newtonsoft.Json;
using OpenQA.Selenium.DevTools;
using Org.BouncyCastle.Crmf;
using practiquesIEI.Entities;
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
    public partial class Búsqueda : Form
    {

        List<centro_educativo> centros;
        public Búsqueda()
        {
            InitializeComponent();
            LoadMap();
             mostrarTodosCentros();
        }

        #region BOTONES
        

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Principal().Show();

        }

        private async void btAceptar_Click(object sender, EventArgs e)
        {
            int total = 0;
            //remueve todos los marcadores que haya en el mapa
            wbMapa.Document.InvokeScript("eval", new object[] { "defaultMarker();" });
            //obtiene los parametros de la búsqueda
            string localidad = tbLocalidad.Text;
            string tipo ="";
            if (cbTipo.SelectedIndex != -1) { tipo = cbTipo.SelectedItem.ToString(); }
            string provincia = tbProv.Text;
            string cod_postal = tbCP.Text;

            //obtiene los centros y los introduce en el dataGridView
            centros = await buscarCentros(localidad, tipo, provincia, cod_postal);
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
                    if (!string.IsNullOrEmpty(localidad) || !string.IsNullOrEmpty(tipo) || !string.IsNullOrEmpty(provincia) || !string.IsNullOrEmpty(cod_postal))
                    {
                        // Llama a la función changeColor aquí
                        changeColor(centro.latitud, centro.longitud);
                    }
                    total++;
                }
            }
            bindingSource1.DataSource = bindinglist;
            lbTotal.Text = total.ToString();

        }
        async Task mostrarTodosCentros() {

            int total = 0;  
            wbMapa.Document.InvokeScript("eval", new object[] { "removeAllMarkers();" });
            //limpia los campos de búsqueda
            tbLocalidad.Text = "";
            cbTipo.SelectedIndex = -1;
            tbProv.Text = "";
            tbCP.Text = "";

            //obtiene todos los centros de la BD y los inserta en el dataGridView
            centros = await todosCentros();
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
                    total++;
                }
            }
            bindingSource1.DataSource = bindinglist;
            lbTotal.Text = total.ToString();    
        }
        async Task<List<centro_educativo>> todosCentros()
        {
            List<centro_educativo> centros = new List<centro_educativo>();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Construye la URL con los parámetros de consulta
                    var apiUrl = "https://localhost:7012/api/BusquedaBBDD/getAllCentros";

                    // Realiza la llamada a la API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verifica si la llamada fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserializa el contenido a un objeto ExtractionResult
                        centros = JsonConvert.DeserializeObject<List<centro_educativo>>(responseContent);


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
            return centros;
        }

        async Task<List<centro_educativo>> buscarCentros(string localidad, string tipo, string provincia, string cod_postal)
        {
            List<centro_educativo> centros = new List<centro_educativo> ();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Construye la URL con los parámetros de consulta
                    var apiUrl = $"https://localhost:7012/api/BusquedaBBDD/findCentros?loc={localidad}&tipo={tipo}&prov={provincia}&cp={cod_postal}";

                    // Realiza la llamada a la API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verifica si la llamada fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserializa el contenido a un objeto ExtractionResult
                        centros = JsonConvert.DeserializeObject<List<centro_educativo>>(responseContent);

  
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
            return centros;
        }

        #endregion
        private void LoadMap()
        {
            string html = $@"<!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset=""utf-8"" />
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <link rel=""stylesheet"" href=""https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"" />
                                <script src=""https://unpkg.com/leaflet@1.7.1/dist/leaflet.js""></script>
                                <style>
                                    #map {{
                                        width: {(810).ToString()}px;
                                        height: {(410).ToString()}px;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div id=""map""></div>
                                <script>
                                    var map = L.map('map').setView([39, 0.8], 5);

                                    L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                                        maxZoom: 25
                                    }}).addTo(map);

                                    L.control.scale().addTo(map);

                                    var markers = [];
                                    var redIcon = new L.Icon({{iconUrl: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
                                        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
                                        iconSize: [20, 35],  // Ajusta el tamaño del icono [width, height]
                                        iconAnchor: [15, 35],
                                        popupAnchor: [1, -17],
                                        shadowSize: [25, 25]

                                                }});

                                    var blueIcon = new L.Icon({{iconUrl: 'https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png',
                                        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
                                        iconSize: [20, 35],  // Ajusta el tamaño del icono [width, height]
                                        iconAnchor: [15, 35],
                                        popupAnchor: [1, -17],
                                        shadowSize: [25, 25]
                                    }});


                                    function addMarker(lat, lng, popupText) {{
                                            try {{
                                                var marker = L.marker([lat, lng]).setIcon(blueIcon).addTo(map);

                                                marker.bindPopup(popupText);
                                                markers.push(marker);
                                            }} catch (error) {{
                                                // Puedes realizar acciones adicionales aquí, como mostrar un mensaje al usuario.
                                            }}
                                        }}

                                    function removeAllMarkers() {{
                                        for (var i = 0; i < markers.length; i++) {{
                                            map.removeLayer(markers[i]); // Elimina el marcador del mapa
                                        }}
                                        markers = []; // Vacía el array de marcadores
                                    }}

                                    function changeMarkerColor(lat, lng, color) {{
                                        for (var i = 0; i < markers.length; i++) {{
                                            var marker = markers[i];
                                            var markerLatLng = marker.getLatLng();

                                            if (markerLatLng.lat === lat && markerLatLng.lng === lng) {{
                                                marker.setIcon(redIcon);
                                            }}
                                        }}
                                    }}

                                    function defaultMarker() {{
                                        for (var i = 0; i < markers.length; i++) {{
                                            var marker = markers[i];
                                            marker.setIcon(blueIcon);
                                        }}
                                    }}


                                    
                                   
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
                if (lat != "" || lng != "") {
                    string script = $"addMarker({lat}, {lng}, \"{popupText}\");";
                    wbMapa.Document.InvokeScript("eval", new object[] { script });
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar marcador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changeColor(string lat, string lng)
        {
            try
            {
                if (lat != "" || lng != "")
                {
                    string script = $"changeMarkerColor({lat}, {lng}, 'red');";
                    wbMapa.Document.InvokeScript("eval", new object[] { script });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar marcador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
