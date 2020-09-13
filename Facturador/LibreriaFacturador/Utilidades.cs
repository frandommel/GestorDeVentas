using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibreriaFacturador
{
    public class Utilidades
    {
        public static DataSet Ejecutar(String cmd)
        {
            SqlConnection Conexion = new SqlConnection("Data Source=.;Initial Catalog=Administracion;Integrated Security=True");
            Conexion.Open();

            DataSet Ds = new DataSet();
            SqlDataAdapter Dp = new SqlDataAdapter(cmd, Conexion);

            Dp.Fill(Ds);

            Conexion.Close();

            return Ds;
        }

    }
}
