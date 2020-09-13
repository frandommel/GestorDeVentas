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
    public partial class VentanaAdmin : VentanaBase
    {
        public VentanaAdmin()
        {
            InitializeComponent();
        }

        private void VentanaAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void VentanaAdmin_Load(object sender, EventArgs e)
        {
            var cmd = String.Format("Select * from Usuario where Id_Usuario = {0}", VentanaLogin.Codigo);
            DataSet Ds = Utilidades.Ejecutar(cmd);

            labelNombreAdmin.Text= Ds.Tables[0].Rows[0]["Nombre_Usuario"].ToString();
            labelUsuario.Text = Ds.Tables[0].Rows[0]["Account"].ToString();
            labelCodigo.Text = VentanaLogin.Codigo;
            pictureBox1.Image = Image.FromFile(Ds.Tables[0].Rows[0]["Foto"].ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContenedorPrincipal Contenedor = new ContenedorPrincipal();
            this.Hide();
            Contenedor.Show();
        }
    }
}
