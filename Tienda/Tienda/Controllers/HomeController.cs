using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Tienda.Permisos;


namespace Tienda.Controllers
{



    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }
        
        public ActionResult ServicioAlCliente()
        {

            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            return View();
        }

        //[ValidarSesion]
        public ActionResult IndexClienteLogeado()
        {
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            

            return View();

        }

    }
}