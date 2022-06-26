using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Tienda.DAO
{
    public class Administrador
    {
        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }

        //----------------------------LOGIN ADMINISTRADOR BASE DE DATOS----------------------------
        public static Models.Administrador GetAdministradorLogin(Models.Administrador admin)
        {
            admin.Contrasena = ConvertirSha256(admin.Contrasena);

            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ValidarAdministrador", cn);
                cmd.Parameters.AddWithValue("Correo", admin.Correo);
                cmd.Parameters.AddWithValue("Contrasena", admin.Contrasena);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                admin.Rut = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            return admin;
        }


        //----------------------------REGISTRO DE EMPLEADO DESDE ADMINISTRADOR BASE DE DATOS----------------------------
        public static bool GetRegistrarEmpleado(Models.Empleado empleado)
        {

            var cantidad = 0;


            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {


                SqlCommand cmd = new SqlCommand("RegistrarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Rut", empleado.Rut);
                cmd.Parameters.AddWithValue("DigitoVerificador", empleado.DigitoVerificador);
                cmd.Parameters.AddWithValue("Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("Nombre_usuario", empleado.Nombre_usuario);
                cmd.Parameters.AddWithValue("Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("fecha_nacimiento", empleado.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("Direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("Estado_civil", empleado.Estado_civil);
                cmd.Parameters.AddWithValue("Contrasena", empleado.Contrasena);
                cmd.Connection = cn;
                cn.Open();

                cantidad = cmd.ExecuteNonQuery();

            }

            return cantidad > 0; 

        }


        //----------------------------ENCRIPTAR CLAVE A SHA256----------------------------

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));

            }

            return Sb.ToString();
        }
    }
}