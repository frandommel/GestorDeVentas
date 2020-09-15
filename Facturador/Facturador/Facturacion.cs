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
    public partial class Facturacion : VentanaProcesos
    {
        public static int contadorFila = 0;
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            var cmd = "Select * from Usuario where Id_Usuario = " + VentanaLogin.Codigo;
            DataSet Ds = Utilidades.Ejecutar(cmd);

            lblAtiende.Text = Ds.Tables[0].Rows[0]["Nombre_Usuario"].ToString();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoCliente.Text.Trim()))
            {
                try
                {
                    string cmd = "Select Nombre_Cliente from Cliente where id_clientes = " + txtCodigoCliente.Text.Trim();
                    DataSet Ds = Utilidades.Ejecutar(cmd);
                    txtCliente.Text = Ds.Tables[0].Rows[0]["Nombre_Cliente"].ToString();
                    txtCodigoProducto.Focus();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error" + error.Message);
                }

            }
            else
            {
                MessageBox.Show("Debes llenar el campo 'Codigo'");
            }
        }

        private void btnColocar_Click(object sender, EventArgs e)
        {
            if (!Utilidades.ValidarFormulario(this, errorProvider1))
            {
                bool existe = false;
                int num_fila = 0;

                if(contadorFila == 0)
                {
                    dataGridView1.Rows.Add(txtCodigoProducto.Text,txtDescripcion.Text,txtPrecio.Text,txtCantidad.Text);
                    double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                    dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                    contadorFila++;
                }
                else
                {
                    foreach(DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        if(Fila.Cells[0].Value.ToString() == txtCodigoProducto.Text)
                        {
                            existe = true;
                            num_fila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataGridView1.Rows[num_fila].Cells[3].Value = (Convert.ToDouble(txtCantidad.Text) + 
                                                                        Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value)).ToString();
                        double importe = Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value);
                        dataGridView1.Rows[num_fila].Cells[4].Value = importe;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(txtCodigoProducto.Text, txtDescripcion.Text, txtPrecio.Text, txtCantidad.Text);
                        double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                        dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                        contadorFila++;
                    }
                }
            }
        }
    }
}
