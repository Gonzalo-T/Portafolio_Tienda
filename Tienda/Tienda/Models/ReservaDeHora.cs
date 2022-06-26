using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class ReservaDeHora
    {
        public int Rut { get; set; }
        public string Correo { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }
    }
}