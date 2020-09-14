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
    public partial class ConsultarCliente : VentanaConsultas
    {
        public ConsultarCliente()
        {
            InitializeComponent();
        }

        private void ConsultarCliente_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LlenarDataGridView("Cliente").Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim()) == false)
            {
                try
                {
                    DataSet Ds;
                    var cmd = "Select * from cliente where Nombre_Cliente like ('%" + textBox1.Text.Trim() + "%')";
                    Ds = Utilidades.Ejecutar(cmd);
                    dataGridView1.DataSource = Ds.Tables[0];
                }
                catch(Exception error)
                {
                    MessageBox.Show("Ha ocurrido un error: " + error.Message);
                }
            }
            else
            {
                dataGridView1.DataSource = LlenarDataGridView("Cliente").Tables[0];
            }
        }
    }
}
