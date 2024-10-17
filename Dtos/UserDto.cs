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
        public int IdUser { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordE { get; set; } //encriptar no se si es necesario
        public int Response { get; set; }
        public string Mensaje { get; set; }

        public bool Respuesta { get; set; }
        public string Apellido { get; set; }

        public string Numero { get; set; }
        public string Correo { get; set; }
        public bool IsSuccessful { get; set; }
    }
}