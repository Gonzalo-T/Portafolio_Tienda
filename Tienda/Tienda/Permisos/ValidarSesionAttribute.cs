using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            /*que inicie desde el login cliente*/
            if (HttpContext.Current.Session["Cliente"] == null)
            {
                filterContext.Result = new RedirectResult("~/Cliente/LoginCliente");
            }
            base.OnActionExecuting(filterContext);

        }
    }


    public class ValidarSesionAdministradorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            /*que inicie desde el login administrador*/
            if (HttpContext.Current.Session["administrador"] == null)
            {
                filterContext.Result = new RedirectResult("~/Administrador/Login");
            }
            base.OnActionExecuting(filterContext);

        }
    }

    public class ValidarSesionEmpleadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            /*que inicie desde el login administrador*/
            if (HttpContext.Current.Session["Empleado"] == null)
            {
                filterContext.Result = new RedirectResult("~/Empleado/LoginEmpleado");
            }
            base.OnActionExecuting(filterContext);

        }
    }

}