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
    public class Empleado
    {
        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }


        //----------------------------QUERY TRAER UN LISTADO DE TODOS LOS EMPLEADOS----------------------------
        public static List<Models.Empleado> GetAll()
        {


            List<Models.Empleado> lstEmpleado = new List<Models.Empleado>();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = "select * from empleado";

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

                        lstEmpleado.Add(new Models.Empleado()
                        {
                            Rut = int.Parse(reader[0].ToString()),
                            DigitoVerificador = reader[1].ToString(),
                            Correo = reader[2].ToString(),
                            Nombre_usuario = reader[3].ToString(),
                            Nombre = reader[4].ToString(),
                            Fecha_nacimiento = reader[5].ToString(),
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

            return lstEmpleado;
        }

        //----------------------------QUERY TRAER POR ID----------------------------
        public static Models.Empleado GetByRut(int Rut)
        {


            Models.Empleado empleado = new  Models.Empleado();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = string.Format("select * from empleado where rut = {0}", Rut);


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

                        empleado = new Models.Empleado()
                        {
                            Rut = int.Parse(reader[0].ToString()),
                            DigitoVerificador = reader[1].ToString(),
                            Correo = reader[2].ToString(),
                            Nombre_usuario = reader[3].ToString(),
                            Nombre = reader[4].ToString(),
                            Fecha_nacimiento = reader[5].ToString(),
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

            return empleado;
        }


        //----------------------------PROCEDIMIENTO BASE DE DATOS LOGIN EMPLEADO----------------------------
        public static Models.Empleado GetEmpleadoLogin(Models.Empleado empleado)
        {
            empleado.Contrasena = ConvertirSha256(empleado.Contrasena);

            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("ValidarEmpleado", cn);
                cmd.Parameters.AddWithValue("Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("Contrasena", empleado.Contrasena);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                empleado.Rut = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                
            }
            return empleado;
        }

        //----------------------------PROCEDIMIENTO Y QUERY UPDATE AND DELETE EMPLOYEE----------------------------
        public static bool Update(Models.Empleado empleado)
        {
            var cantidad = 0;
            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ActualizarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Rut", empleado.Rut);
                cmd.Parameters.AddWithValue("Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("Nombre_usuario", empleado.Nombre_usuario);
                cmd.Parameters.AddWithValue("Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("fecha_nacimiento", empleado.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("Direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("Estado_civil", empleado.Estado_civil);
                cmd.Connection = cn;
                cn.Open();

                cantidad = cmd.ExecuteNonQuery();
            }
            return cantidad > 0;
        }

        public static bool Delete(Models.Empleado empleado)
        {

            var cantidad = 0;
            string queryString = string.Format("Delete from empleado where rut = {0}", empleado.Rut);

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


        //----------------------------LISTADO DE TODOS LOS CONTACTOS DE LOS CLIENTES----------------------------
        public static List<Models.Contacto> ContactoAll()
        {


            List<Models.Contacto> lstContacto = new List<Models.Contacto>();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = "select * from contacto";

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

                        lstContacto.Add(new Models.Contacto()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Nombre = reader[1].ToString(),
                            Apellido = reader[2].ToString(),
                            Correo = reader[3].ToString(),
                            Telefono = int.Parse(reader[4].ToString()),
                            Mensaje = reader[5].ToString(),
                            

                        });

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstContacto;
        }

        //----------------------------TRAER EL CONTACTO POR SU ID PARA ELIMINARLO----------------------------
        public static Models.Contacto GetById(int Id)
        {


            Models.Contacto empleado = new Models.Contacto();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = string.Format("select * from contacto where id = {0}", Id);


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

                        empleado = new Models.Contacto()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Nombre = reader[1].ToString(),
                            Apellido = reader[2].ToString(),
                            Correo = reader[3].ToString(),
                            Telefono = int.Parse(reader[4].ToString()),
                            Mensaje = reader[5].ToString(),


                        };

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return empleado;
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


        //----------------------------BORRAR CONTACTO----------------------------
        public static bool DeleteContacto(Models.Contacto contacto)
        {

            var cantidad = 0;
            string queryString = string.Format("Delete from contacto where id = {0}", contacto.Id);

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


    }


}