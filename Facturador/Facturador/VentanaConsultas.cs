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
    public partial class VentanaConsultas : VentanaBase
    {
        public VentanaConsultas()
        {
            InitializeComponent();
        }

        public DataSet LlenarDataGridView(string tabla)
        {
            DataSet Ds;

            try
            {
                var cmd = "SELECT * FROM "+tabla;
                Ds = Utilidades.Ejecutar(cmd);
                return Ds;
            }
            catch(Exception error)
            {
                MessageBox.Show("No existe la tabla seleccionada" + error.Message);
                return null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
