using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturador
{
    public partial class VentanaBase : Form
    {
        public VentanaBase()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Desea Saliir?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        public virtual void Eliminar()
        {

        }
        public virtual void Nuevo()
        {

        }
        public virtual void Consultar()
        {

        }
        public virtual bool Guardar()
        {
            return false;
        }
    }
}
