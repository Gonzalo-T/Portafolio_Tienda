using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Tienda.Models;

using System.Data.SqlClient;
using System.Data;
using Tienda.Permisos;

namespace Tienda.Controllers
{
    public class AccesoController : Controller
    {

        
        

        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}