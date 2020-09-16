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
    public partial class Reporte : VentanaBase
    {
        public DataSet data;
        public Reporte()
        {
            InitializeComponent();
        }
        public Reporte(DataSet ds)
        {
            InitializeComponent();
            data = ds;
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.data.Tables[0];
        }
    }
}
