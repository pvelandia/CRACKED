using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class UserDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        public int IdEstado { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Apellido { get; set; }

        public string Numero { get; set; }
        public string Correo { get; set; }

        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;


        public bool Respuesta { get; set; }

        public string Mensaje { get; set; }
    }
}