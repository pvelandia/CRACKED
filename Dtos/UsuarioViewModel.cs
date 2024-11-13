using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroContacto { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdEstado { get; set; }
        public int IdRol { get; set; }
    }

}