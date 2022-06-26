using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;
using Tienda.Permisos;

namespace Tienda.Controllers
{
    public class ContactoController : Controller
    {

        //----------------------------VISTAS PARA FORMULARIO CONTACTENOS REGISTRADO----------------------------

        [ValidarSesion]
        public ActionResult Contactenos()
        {
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            return View();
        }

        [HttpPost]
        public ActionResult Contactenos(Contacto contacto)
        {

            if (DAO.Contacto.GetRegistrarContacto(contacto))
            {


                return RedirectToAction("Contactenos", "Contacto");

            }
            else
            {
                //DAO.Administrador.GetRegistrarEmpleado(empleado);
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }

        }

        


        //----------------------------VISTAS PARA FORMULARIO CONTACTENOS NO REGISTRADO----------------------------
        public ActionResult ContactenosNoRegistrado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactenosNoRegistrado(Contacto contacto)
        {

            if (DAO.Contacto.GetRegistrarContacto(contacto))
            {


                return RedirectToAction("ContactenosNoRegistrado", "Contacto");

            }
            else
            {
                //DAO.Administrador.GetRegistrarEmpleado(empleado);
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }

            


        }



        //----------------------------CERRAR SESION----------------------------
        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            return RedirectToAction("Index", "Home");
        }


    }



}
