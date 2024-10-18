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

            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]

            public string Name { get; set; } = string.Empty;
            [Required(ErrorMessage = "Confirme la contraseña.")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]


            public string ConfirmPassword { get; set; } = string.Empty;


            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
            [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número.")]
            public string Password { get; set; }

            public string PasswordE { get; set; } //encriptar no se si es necesario
            public int Response { get; set; }
            public string Mensaje { get; set; }

            public bool Respuesta { get; set; }
            [Required(ErrorMessage = "El apellido es obligatorio.")]
            [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
            public string Apellido { get; set; }
            [Required(ErrorMessage = "El número es obligatorio.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Número inválido, debe contener 10 dígitos.")]
            public string Numero { get; set; }
            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
            public string Correo { get; set; }
            public bool IsSuccessful { get; set; }

        }
    }