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
    public class EmpleadoController : Controller
    {
        //----------------------------VISTAS LOGIN EMPLEADO----------------------------
        public ActionResult LoginEmpleado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginEmpleado(Empleado empleado)
        {
            empleado = DAO.Empleado.GetEmpleadoLogin(empleado);

            if (empleado.Rut != 0)
            {

                Session["Empleado"] = empleado.Correo;
                return RedirectToAction("ServiciosEmpleado", "Empleado");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
        }


        

        //----------------------------VISTA DE EMPLEADO PARA ELEGIR SERVICIOS----------------------------


        [ValidarSesionEmpleado]
        public ActionResult ServiciosEmpleado()
        {
            string usernameEmpleado = "" + Session["Empleado"];
            ViewBag.empleado = usernameEmpleado;
            return View();
        }

        //----------------------------CERRAR SESION DE EMPLEADO----------------------------


        public ActionResult CerrarSesion()
        {
            Session["Empleado"] = null;
            return RedirectToAction("Index", "Home");
        }


        //----------------------------TABLA DE CONTACTO DE CLIENTES----------------------------

        public ActionResult TablaContacto()
        {
            List<Models.Contacto> lstContacto = DAO.Empleado.ContactoAll();
            return View(lstContacto);
        }

        //----------------------------VISTA  CONTACTO DE CLIENTES POR ID----------------------------
        public ActionResult EliminarContacto(int Id)
        {

            string adminname = "" + Session["Empleado"];
            ViewBag.admin = adminname;

            Models.Contacto contacto = DAO.Empleado.GetById(Id);
            return View(contacto);
        }

        //----------------------------ELIMINAR EL CONTACTO DE CLIENTES POR SU ID, RETORNA LA TABLA----------------------------
        [HttpPost]
        public ActionResult EliminarContacto(Contacto contacto)
        {
            DAO.Empleado.DeleteContacto(contacto);
            return RedirectToAction("TablaContacto", "Empleado");
        }


        //----------------------------INGRESAR PRODUCTO----------------------------
        public ActionResult IngresarProducto()
        {
            string usernameEmpleado = "" + Session["Empleado"];
            ViewBag.empleado = usernameEmpleado;
            return View();
        }

        [HttpPost]
        public ActionResult IngresarProducto(Productos producto)
        {
            

            string usernameEmpleado = "" + Session["Empleado"];
            ViewBag.empleado = usernameEmpleado;

            producto = DAO.Productos.GetIngresarProducto(producto);


            switch (producto.Nombre)
            {
                case "1":
                    ViewData["Mensaje"] = "Ya tiene producto ingresado con ese nombre";
                    break;
                case "2":
                    ViewData["Mensaje"] = "Su producto ha sido ingresado con éxito";
                    break;

            }


            return View();

        }


    }
}
