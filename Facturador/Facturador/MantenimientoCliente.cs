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
    public partial class MantenimientoCliente : VentanaMantenimiento
    {
        public MantenimientoCliente()
        {
            InitializeComponent();
        }
        public override bool Guardar()
        {
            if(Utilidades.ValidarFormulario(this,errorProvider1) == false)
            {
                try
                {
                    var cmd = String.Format("EXEC ActualizaClientes'{0}','{1}','{2}'",
                              txtIdCliente.Text.Trim(), txtNombre.Text.Trim(), txtApellido.Text.Trim());
                    Utilidades.Ejecutar(cmd);
                    MessageBox.Show("Se ha guardado correctamente!..");
                    txtIdCliente.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtIdCliente.Focus();
                    return true;
                }
                catch (Exception error)
                {
                    MessageBox.Show("Ha ocurrido un error " + error.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override void Eliminar()
        {
            try
            {
                var cmd = String.Format("EXEC EliminarClientes '{0}'", txtIdCliente.Text.Trim());
                Utilidades.Ejecutar(cmd);
                MessageBox.Show("Se ha eliminado el cliente con exito");
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error " + error.Message);
            }
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
