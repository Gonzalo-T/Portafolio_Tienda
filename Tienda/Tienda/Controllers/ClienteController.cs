using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Tienda.Models;
namespace Tienda.Controllers
{

    public class ClienteController : Controller
    {
        //--------------------------------------LOGIN CLIENTE--------------------------
        public ActionResult LoginCliente()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginCliente(Cliente cliente)
        {
            cliente = DAO.Cliente.GetClienteLogin(cliente);



            if (cliente.Rut != 0)
            {
                
                Session["Cliente"] = cliente.Correo;
                Session["Cliente1"] = cliente.Rut;



                return RedirectToAction("IndexClienteLogeado", "Home");
            }


            else
            {
                ViewData["Mensaje"] = "usuario o contraseña incorrecto";
                return View();
            }


        }


        //--------------------------------------REGISTRAR CLIENTE--------------------------
        public ActionResult RegistrarCliente()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegistrarCliente(Cliente cliente)
        {
            if (cliente.Contrasena == cliente.ConfirmarContrasena)
            {

                cliente.Contrasena = ConvertirSha256(cliente.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }




            if (DAO.Cliente.GetRegistrarCliente(cliente))
            {


                return RedirectToAction("Redirigido", "Cliente");

            }
            else
            {
                //DAO.Administrador.GetRegistrarEmpleado(empleado);
                ViewData["Mensaje"] = "usuario ya se encuentra registrado";
                return View();
            }

        }


        public ActionResult Redirigido()
        {
            return View();
        }

        //----------------------------ENCRIPTACIÓN DE CLAVE FORMATO SHA256----------------------------
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

        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
