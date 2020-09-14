using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaFacturador;
namespace Facturador
{
    public partial class ConsultarProducto : VentanaConsultas
    {
        public ConsultarProducto()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) == false)
            {
                try
                {
                    DataSet Ds;
                    var cmd = "Select * from Articulo where Nombre_Producto like ('%" + textBox1.Text.Trim() + "%')";
                    Ds = Utilidades.Ejecutar(cmd);
                    dataGridView1.DataSource = Ds.Tables[0];
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Ha ocurrido un error: " + error.Message);
                }
            }
            else
            {
                dataGridView1.DataSource = LlenarDataGridView("Articulo").Tables[0];
            }
        }

        private void ConsultarProducto_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LlenarDataGridView("Articulo").Tables[0];
        }
    }
}
