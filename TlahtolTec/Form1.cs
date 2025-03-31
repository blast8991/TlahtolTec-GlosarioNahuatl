using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TlahtolTec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            ActualizarDataGrid();
        }
        private void ActualizarDataGrid()
        {
            dgvPalabras.DataSource = DatabaseHelper.ObtenerTodas();
        }

        private void LimpiarCampos()
        {
            txtEspanol.Clear();
            txtNahuatl.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTraducir_Click(object sender, EventArgs e)
        {
            string palabra = txtBusqueda.Text.Trim();
            if (!string.IsNullOrEmpty(palabra))
            {
                string traduccion = DatabaseHelper.Traducir(palabra);
                if (traduccion != null)
                {
                    txtEspanol.Text = palabra;
                    txtNahuatl.Text = traduccion;
                }
                else
                {
                    MessageBox.Show("Palabra no encontrada.");
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string espanol = txtEspanol.Text.Trim();
            string nahuatl = txtNahuatl.Text.Trim();

            if (!string.IsNullOrEmpty(espanol) && !string.IsNullOrEmpty(nahuatl))
            {
                DatabaseHelper.AgregarPalabra(espanol, nahuatl);
                ActualizarDataGrid();
                LimpiarCampos();
            }
        }
    }
}
