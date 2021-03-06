using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Cliente
    {
        public int Rut { get; set; }

        public string DigitoVerificador { get; set; }
        public string Correo { get; set; }


        public string Nombre_usuario { get; set; }
        public string Nombre { get; set; }
        public string Fecha_nacimiento { get; set; }
        public string Direccion { get; set; }
        public string Estado_civil { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}