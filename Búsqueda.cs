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
            //ConexionBD.Conectar();
            InitializeComponent();
            string path = Path.Combine(Application.StartupPath, "index.html");
            wbMapa.Navigate(path);
        }

        #region BOTONES

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            centros = ConexionBD.buscarCentros(tbLocalidad.Text, int.Parse(tbCP.Text), tbProv.Text, cbTipo.SelectedValue.ToString());
            foreach (var centro in centros) {
                tbLogs.Text += $"Centro cargado: {centro.nombre} " ;
            }

        }

        private void btCarga_Click(object sender, EventArgs e)
        {

        }

        private void btCerrar_Click(object sender, EventArgs e)
        {

        }

        #endregion



    }
}
