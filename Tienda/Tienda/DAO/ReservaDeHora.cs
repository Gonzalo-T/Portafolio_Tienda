using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tienda.DAO
{
    public class ReservaDeHora
    {
        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }

        //----------------------------PROCEDIMIENTO BASE DE DATOS RESERVA DE HORA DE CLIENTE----------------------------
        public static Models.ReservaDeHora GetReservarHora(Models.ReservaDeHora reserva)
        {


            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ReservaHora", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Rut", reserva.Rut);
                cmd.Parameters.AddWithValue("Correo", reserva.Correo);
                cmd.Parameters.AddWithValue("Fecha", reserva.Fecha);
                cmd.Parameters.AddWithValue("Hora", reserva.Hora);

                cmd.Connection = cn;
                //cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                // cmd.Parameters.Add("MensajeIf", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                cn.Open();


                reserva.Correo = Convert.ToString(cmd.ExecuteScalar());


            }

            return reserva;

        }

        public static List<Models.ReservaDeHora> GetAll(int Rut)
        {


            List<Models.ReservaDeHora> lstReserva = new List<Models.ReservaDeHora>();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = string.Format("select * from reserva where rut = {0}", Rut);

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

                        lstReserva.Add(new Models.ReservaDeHora()
                        {
                            Rut = int.Parse(reader[1].ToString()),
                            Correo = reader[2].ToString(),
                            Fecha = reader[3].ToString(),
                            Hora = reader[4].ToString()


                        });

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstReserva;
        }


        public static bool EliminarReserva(Models.ReservaDeHora reserva)
        {

            var cantidad = 0;
            string queryString = string.Format("Delete from reserva where rut = {0}", reserva.Rut);

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

        public static Models.ReservaDeHora GetByRut(int Rut)
        {


            Models.ReservaDeHora reserva = new Models.ReservaDeHora();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";


            // Provide the query string with a parameter placeholder.
            string queryString = string.Format("select * from Reserva where rut = {0}", Rut);


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

                        reserva = new Models.ReservaDeHora()
                        {
                            Rut = int.Parse(reader[1].ToString()),
                            Correo = reader[2].ToString(),
                            Fecha = reader[3].ToString(),
                            Hora = reader[4].ToString()

                        };

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return reserva;
        }
    }
}