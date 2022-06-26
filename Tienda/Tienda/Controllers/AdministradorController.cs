using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;
using Tienda.Permisos;

namespace Tienda.Controllers
{
    public class AdministradorController : Controller
    {


        //---------------VISTAS LOGIN ADMINISTRADOR-------------------------------
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Administrador admin)
        {
            admin = DAO.Administrador.GetAdministradorLogin(admin);

            if (admin.Rut != 0)
            {

                Session["administrador"] = admin.Correo;
                return RedirectToAction("SeleccionarAdministrador", "Administrador");
            }


            else
            {
                ViewData["Mensaje"] = "usuario o contraseña incorrecto";
                return View();
            }


        }



        //-----------------VISTAS DE SELECCION Y REDIRIGIDO---------------------------------

        //[ValidarSesionAdministrador]
        public ActionResult SeleccionarAdministrador()
        {
            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;
            return View();
        }

        public ActionResult RedirigidoListadoEmpleado()
        {
            return View();
        }


        // [ValidarSesionAdministrador]

        public ActionResult RedirigidoListadoCliente()
        {
            return View();
        }

        public ActionResult RedirigidoListadoClienteEliminado()
        {
            return View();
        }
        public ActionResult RedirigidoRegistroEmpleado()
        {


            return View();
        }

        public ActionResult RedirigidoListadoEmpleadoEliminado()
        {


            return View();
        }


        //-----------------------------------------VISTA LISTADO DE EMPLEADOS---------------------------------
        public ActionResult ListadoEmpleados()
        {
            List<Models.Empleado> lstEmpleado = DAO.Empleado.GetAll();

            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;

            return View(lstEmpleado);


        }

        //-----------------------------------------VISTA LISTADO DE CLIENTES---------------------------------
        public ActionResult ListadoClientes()
        {
            List<Models.Cliente> lstCliente = DAO.Cliente.GetAll();

            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;

            return View(lstCliente);


        }



        //--------------------------------------REGISTRAR EMPLEADO--------------------------
        // [ValidarSesionAdministrador]
        public ActionResult RegistrarEmpleado()
        {
            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;
            return View();
        }


        [HttpPost]
        public ActionResult RegistrarEmpleado(Empleado empleado)
        {


            if (empleado.Contrasena == empleado.ConfirmarContrasena)
            {

                empleado.Contrasena = ConvertirSha256(empleado.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }



            ViewData["Mensaje"] = "Empleado ya existe en el sistema";

            if (DAO.Administrador.GetRegistrarEmpleado(empleado))
            {

                
                return RedirectToAction("RedirigidoRegistroEmpleado", "Administrador");
                
            }
            else
            {
                //DAO.Administrador.GetRegistrarEmpleado(empleado);
                ViewData["Mensaje"] = "Empleado ya existe en el sistema";
                return View();
            }

        }


        //--------------------------------------Actualizar EMPLEADO--------------------------
        //[ValidarSesionAdministrador]
        public ActionResult ActualizarEmpleado(int Rut)
        {


            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;


            Models.Empleado empleado = DAO.Empleado.GetByRut(Rut);

            return View(empleado);


        }


        [HttpPost]
        public ActionResult ActualizarEmpleado(Empleado empleado)
        {
            DAO.Empleado.Update(empleado);
            return RedirectToAction("RedirigidoListadoEmpleado", "Administrador");
        }

        //--------------------------------------Actualizar CLIENTE--------------------------
        //[ValidarSesionAdministrador]
        public ActionResult ActualizarCliente(int Rut)
        {


            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;


            Models.Cliente cliente = DAO.Cliente.GetByRut(Rut);

            return View(cliente);


        }


        [HttpPost]
        public ActionResult ActualizarCliente(Cliente cliente)
        {
            DAO.Cliente.Update(cliente);
            return RedirectToAction("RedirigidoListadoCliente", "Administrador");
        }




        //--------------------------------------ELIMINAR EMPLEADO--------------------------

        public ActionResult EliminarEmpleado(int Rut)
        {
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;

            Models.Empleado empleado = DAO.Empleado.GetByRut(Rut);

            return View(empleado);
        }

        [HttpPost]
        public ActionResult EliminarEmpleado(Empleado empleado)
        {
            DAO.Empleado.Delete(empleado);
            return RedirectToAction("RedirigidoListadoEmpleadoEliminado", "Administrador");
        }

        //--------------------------------------ELIMINAR CLIENTE--------------------------

        public ActionResult EliminarCliente(int Rut)
        {
            string adminname = "" + Session["administrador"];
            ViewBag.admin = adminname;

            Models.Cliente cliente = DAO.Cliente.GetByRut(Rut);

            return View(cliente);
        }

        [HttpPost]
        public ActionResult EliminarCliente(Cliente cliente)
        {
            DAO.Cliente.Delete(cliente);
            return RedirectToAction("RedirigidoListadoClienteEliminado", "Administrador");
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

      
    }
}
