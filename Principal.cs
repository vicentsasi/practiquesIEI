using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practiquesIEI
{
    public partial class Principal : Form
    {
        public Principal()
        {
            //Conecta en la BD i borra els datos existents(esta tot dins del metodo conectar)
            Load();
            InitializeComponent();
        }
        public static async Task Load() {
            await ConexionBD.Conectar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Carga().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Búsqueda().Show();
        }
    }
}
