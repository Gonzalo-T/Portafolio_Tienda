using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Tienda.Models
{
    public class Contacto
    {
       public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Correo { get; set; }
     
        public int Telefono { get; set; }
        public string Mensaje { get; set; }
    }
}