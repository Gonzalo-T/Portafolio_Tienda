using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Tienda.DAO
{
    public class Productos
    {

        public static string CadenaConexion
        {
            get { return "Data Source=IBM-PF3610T8;Initial Catalog=test1;Integrated Security=true"; }
        }
        //----------------------------OBTENER EL LISTADO DE LOS PRODUCTOS IGRESADOS----------------------------
        public static List<Models.Productos> GetAll()
        {
            

            List<Models.Productos> lstProductos = new List<Models.Productos>();

            string connectionString =
                                    "Data Source=(local);Initial Catalog=test1;"
                                    + "Integrated Security=true";

            
            // Provide the query string with a parameter placeholder.
            string queryString = "select * from productos";

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
                        
                        lstProductos.Add(new Models.Productos()
                        {
                            
                            Nombre = reader[1].ToString(),
                            Descripcion = reader[2].ToString(),
                            Precio = int.Parse(reader[3].ToString()),
                            Imagen = Convert.ToBase64String((byte[])reader[4])






                    });

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lstProductos;
        }


        public static Models.Productos GetIngresarProducto(Models.Productos producto)
        {


            using (SqlConnection cn = new SqlConnection(CadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("IngresarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("Precio", producto.Precio);
                cmd.Parameters.AddWithValue("Imagen",  producto.Imagen);

                cmd.Connection = cn;
                //cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                // cmd.Parameters.Add("MensajeIf", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                cn.Open();


                producto.Nombre = Convert.ToString(cmd.ExecuteScalar());


            }

            return producto;

        }
    }
}