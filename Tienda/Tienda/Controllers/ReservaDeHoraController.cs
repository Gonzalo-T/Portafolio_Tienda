using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ReservaDeHoraController : Controller
    {
        // GET: ReservaDeHora
        public ActionResult Reserva()
        {
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            string RutCliente = "" + Session["Cliente1"];
            ViewBag.userRut = RutCliente;

            return View();
        }

        public ActionResult RedirigidoReserva()
        {
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            string RutCliente = "" + Session["Cliente1"];
            ViewBag.userRut = RutCliente;

            return View();
        }

        [HttpPost]
        public ActionResult Reserva(ReservaDeHora reserva)
        {
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            string RutCliente = "" + Session["Cliente1"];
            ViewBag.userRut = RutCliente;

            reserva = DAO.ReservaDeHora.GetReservarHora(reserva);


            switch (reserva.Correo)
            {
                case "1":
                    ViewData["Mensaje"] = "Ya tiene una hora agendada a su nombre";
                    break;
                case "2":
                    ViewData["Mensaje"] = "Ya La hora se encuentra ocupada";
                    break;
                case "3":
                    ViewData["Mensaje"] = "Su hora fue agendada con éxito";
                    break;
            }

            
            return View();

        }

        public ActionResult ListadoReserva(int Rut)
        {

            List<Models.ReservaDeHora> lstReserva = DAO.ReservaDeHora.GetAll(Rut);
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            string RutCliente = "" + Session["Cliente1"];
            ViewBag.userRut = RutCliente;

      

            return View(lstReserva);
        }

        public ActionResult EliminarHora(int Rut)
        {

            Models.ReservaDeHora reserva = DAO.ReservaDeHora.GetByRut(Rut);
            string usernameCliente = "" + Session["Cliente"];
            ViewBag.user = usernameCliente;

            string RutCliente = "" + Session["Cliente1"];
            ViewBag.userRut = RutCliente;



            return View(reserva);
        }



        [HttpPost]
        public ActionResult EliminarHora(ReservaDeHora reserva)


        {
            DAO.ReservaDeHora.EliminarReserva(reserva);

         
            return RedirectToAction("RedirigidoReserva", "ReservaDeHora");
        }
    }
}
