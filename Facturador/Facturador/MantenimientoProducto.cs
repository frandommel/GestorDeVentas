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
    public partial class MantenimientoProducto : VentanaMantenimiento
    {
        public MantenimientoProducto()
        {
            InitializeComponent();
        }

        public override bool Guardar()
        {
            try
            {
                var cmd = String.Format("EXEC ActualizaArticulos'{0}','{1}','{2}'",
                          txtIdProducto.Text.Trim(),txtNombre.Text.Trim(),txtPrecio.Text.Trim());
                Utilidades.Ejecutar(cmd);
                MessageBox.Show("Se ha guardado correctamente!..");
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error " + error.Message);
                return false;
            }
        }
        public override void Eliminar()
        {
            try
            {
                var cmd = String.Format("EXEC EliminarArticulos '{0}'", txtIdProducto.Text.Trim());
                Utilidades.Ejecutar(cmd);
                MessageBox.Show("Se ha eliminado el producto con exito");
            }catch(Exception error)
            {
                MessageBox.Show("Ha ocurrido un error " + error.Message);
            }
        }
    }
}
