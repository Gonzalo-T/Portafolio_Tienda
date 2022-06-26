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
    public class Cliente
    {

        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }


        //----------------------------PROCEDIMIENTO BASE DE DATOS LOGIN CLIENTE----------------------------
        public static Models.Cliente GetClienteLogin(Models.Cliente cliente)
        {
            cliente.Contrasena = ConvertirSha256(cliente.Contrasena);

            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ValidarCliente", cn);
                cmd.Parameters.AddWithValue("Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("Contrasena", cliente.Contrasena);


                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                
                cliente.Rut = Convert.ToInt32(cmd.ExecuteScalar().ToString());



            }

            return cliente;
        }

        //----------------------------PROCEDIMIENTO BASE DE DATOS REGISTRO DE CLIENTE----------------------------

        public static bool GetRegistrarCliente(Models.Cliente cliente)
        {

            var cantidad = 0;


            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {


                SqlCommand cmd = new SqlCommand("RegistrarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Rut", cliente.Rut);
                cmd.Parameters.AddWithValue("DigitoVerificador", cliente.DigitoVerificador);
                cmd.Parameters.AddWithValue("Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("Nombre_usuario", cliente.Nombre_usuario);
                cmd.Parameters.AddWithValue("Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("fecha_nacimiento", cliente.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("Estado_civil", cliente.Estado_civil);
                cmd.Parameters.AddWithValue("Contrasena", cliente.Contrasena);
                cmd.Connection = cn;
                cn.Open();

                cantidad = cmd.ExecuteNonQuery();

            }

            return cantidad > 0;

        }


        //----------------------------QUERY TRAER UN LISTADO DE TODOS LOS Clientes----------------------------
        public static List<Models.Cliente> GetAll()
        {


            List<Models.Cliente> lstCliente = new List<Models.Cliente>();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = "select * from cliente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {

                        lstCliente.Add(new Models.Cliente()
                        {
                            Rut = int.Parse(reader[0].ToString()),
                            DigitoVerificador = reader[1].ToString(),
                            Correo = reader[2].ToString(),
                            Nombre_usuario = reader[3].ToString(),
                            Nombre = reader[4].ToString(),
                            Fecha_nacimiento = Convert.ToDateTime(reader[5]).ToString("dd/MM/yyyy"),
                            Direccion = reader[6].ToString(),
                            Estado_civil = reader[7].ToString(),
                            Contrasena = reader[8].ToString()

                        });

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstCliente;
        }


        //----------------------------QUERY TRAER POR ID----------------------------
        public static Models.Cliente GetByRut(int Rut)
        {


            Models.Cliente cliente = new Models.Cliente();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = string.Format("select * from cliente where rut = {0}", Rut);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {

                        cliente = new Models.Cliente()
                        {
                            Rut = int.Parse(reader[0].ToString()),
                            DigitoVerificador = reader[1].ToString(),
                            Correo = reader[2].ToString(),
                            Nombre_usuario = reader[3].ToString(),
                            Nombre = reader[4].ToString(),
                            Fecha_nacimiento = Convert.ToDateTime(reader[5]).ToString("dd/MM/yyyy"),
                            Direccion = reader[6].ToString(),
                            Estado_civil = reader[7].ToString(),
                            Contrasena = reader[8].ToString()

                        };

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return cliente;
        }


        //----------------------------PROCEDIMIENTO Y QUERY UPDATE AND DELETE CLIENTE----------------------------
        public static bool Update(Models.Cliente cliente)
        {
            var cantidad = 0;
            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ActualizarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Rut", cliente.Rut);
                cmd.Parameters.AddWithValue("Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("Nombre_usuario", cliente.Nombre_usuario);
                cmd.Parameters.AddWithValue("Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("fecha_nacimiento", cliente.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("Estado_civil", cliente.Estado_civil);
                cmd.Connection = cn;
                cn.Open();

                cantidad = cmd.ExecuteNonQuery();
            }
            return cantidad > 0;

        }

        public static bool Delete(Models.Cliente cliente)
        {

            var cantidad = 0;
            string queryString = string.Format("Delete from cliente where rut = {0}", cliente.Rut);

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    cantidad = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return cantidad > 0;
        }
        //----------------------------ENCRIPTACIÓN DE CLAVE A SHA256----------------------------

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