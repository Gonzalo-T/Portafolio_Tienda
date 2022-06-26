using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tienda.DAO
{
    public class Contacto
    {
        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }

        //----------------------------PROCEDIMIENTO BASE DE DATOS REGISTRAR CONTACTO DE CLIENTE----------------------------
        public static bool GetRegistrarContacto(Models.Contacto contacto)
        {

            var cantidad = 0;


            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("RegistrarContacto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", contacto.Apellido);
                cmd.Parameters.AddWithValue("Correo", contacto.Correo);
                cmd.Parameters.AddWithValue("Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("Mensaje", contacto.Mensaje);
                cmd.Connection = cn;
                //cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                // cmd.Parameters.Add("MensajeIf", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                

                cn.Open();


                cantidad = cmd.ExecuteNonQuery();


            }

            return cantidad > 0;

        }

       

    }
}