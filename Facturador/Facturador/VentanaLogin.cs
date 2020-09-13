using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibreriaFacturador;
namespace Facturador
{
    public partial class VentanaLogin : VentanaBase
    {
        public static string Codigo { get; set; }
        public VentanaLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var Cmd = string.Format("Select * from Usuario where Account='{0}' AND Password='{1}'",
                    txtAccount.Text.Trim(), txtPassword.Text.Trim());
                DataSet data = Utilidades.Ejecutar(Cmd);

                Codigo = data.Tables[0].Rows[0]["Id_Usuario"].ToString().Trim();
                var cuenta = data.Tables[0].Rows[0]["Account"].ToString().Trim();
                var password = data.Tables[0].Rows[0]["Password"].ToString().Trim();

                if(cuenta == txtAccount.Text.Trim() && password == txtPassword.Text.Trim())
                {
                    if ((bool)data.Tables[0].Rows[0]["Status_Admin"] == true)
                    {
                        VentanaAdmin Admin = new VentanaAdmin();
                        this.Hide();
                        Admin.Show();
                    }
                    else
                    {
                        VentanaUser User = new VentanaUser();
                        this.Hide();
                        User.Show();
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Usuario o Contraseña incorrecta");
                txtAccount.Text = "";
                txtPassword.Text = "";
                txtAccount.Focus();
            }
        }

        private void VentanaLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
