using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
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
        public static Boolean ValidarFormulario(Control objeto, ErrorProvider Error)
        {
            bool HayErrores = false;

            foreach (Control Item in objeto.Controls)
            {
                if (Item is ErrorTxtBox)
                {
                    ErrorTxtBox Obj = (ErrorTxtBox)Item;

                    if (Obj.Validar == true)
                    {
                        if (string.IsNullOrEmpty(Obj.Text.Trim()))
                        {
                            Error.SetError(Obj, "No puede estar vacio");
                            HayErrores = true;
                        }
                    }
                    if(Obj.SoloNumeros == true)
                    {
                        int contador=0, LetrasEncontradas = 0;

                        foreach(char letra in Obj.Text.Trim())
                        {
                            if (char.IsLetter(Obj.Text.Trim(), contador))
                            {
                                LetrasEncontradas++;
                            }
                            contador++;
                        }

                        if(LetrasEncontradas != 0)
                        {
                            HayErrores = true;
                            Error.SetError(Obj, "Solo numeros");
                        }
                    }
                }
            }
            return HayErrores;
        }
    }

}
