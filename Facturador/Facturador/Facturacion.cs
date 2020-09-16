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
        public static double total;
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
            total = 0;
            foreach (DataGridViewRow Fila in dataGridView1.Rows)
            {
                total += (double)Fila.Cells[4].Value;
            }
            lblTotal.Text = total.ToString();
            txtCodigoProducto.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(contadorFila > 0)
            {
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value));
                lblTotal.Text = total.ToString();

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);

                contadorFila--;
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            ConsultarCliente consultarCliente = new ConsultarCliente();
            consultarCliente.ShowDialog();

            if(consultarCliente.DialogResult == DialogResult.OK)
            {
                txtCodigoCliente.Text = consultarCliente.dataGridView1.Rows[consultarCliente.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                txtCliente.Text = consultarCliente.dataGridView1.Rows[consultarCliente.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                txtCodigoProducto.Focus();
            }
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            ConsultarProducto consultarProducto = new ConsultarProducto();
            consultarProducto.ShowDialog();

            if(consultarProducto.DialogResult == DialogResult.OK)
            {
                txtCodigoProducto.Text = consultarProducto.dataGridView1.Rows[consultarProducto.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                txtDescripcion.Text = consultarProducto.dataGridView1.Rows[consultarProducto.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                txtPrecio.Text = consultarProducto.dataGridView1.Rows[consultarProducto.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
                txtCantidad.Focus();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        public override void Nuevo()
        {
            txtCodigoCliente.Text = "";
            txtCliente.Text = "";
            txtCodigoProducto.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            lblTotal.Text = "0";
            dataGridView1.Rows.Clear();
            contadorFila = 0;
            total = 0;
            txtCodigoCliente.Focus();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if(contadorFila != 0)
            {
                try
                {
                    string cmd = string.Format("Exec ActualizaFacturas '{0}'", txtCodigoCliente.Text.Trim());
                    DataSet Ds = Utilidades.Ejecutar(cmd);

                    String numFac = Ds.Tables[0].Rows[0]["NumFac"].ToString().Trim();

                    foreach(DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        cmd = string.Format("Exec ActualizaDetalles '{0}','{1}','{2}','{3}'",numFac,Fila.Cells[0].Value.ToString(), Fila.Cells[2].Value.ToString(), Fila.Cells[3].Value.ToString());
                        Ds = Utilidades.Ejecutar(cmd);
                    }
                    cmd = "Exec DatosFactura " + numFac;

                    Ds = Utilidades.Ejecutar(cmd);

                    Reporte rp = new Reporte(Ds);
                    rp.ShowDialog();
                    Nuevo();
                }
                catch(Exception error)
                {
                    MessageBox.Show("Error: " + error.Message);
                }
            }
        }
    }
}
