using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using CRACKED.Utilities;
using System;
using System.Linq;

namespace CRACKED.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserListDto ListarUsuarios()
        {
            return _userRepository.ListarUsuarios();
        }

        public UserDto RegistroUsuarios(UserDto usuario)
        {
            try
            {
                if (usuario.Password != usuario.ConfirmPassword)
                    throw new Exception("Las contraseñas no coinciden. Por favor, verifícalas.");

                if (_userRepository.BuscarUsuario(usuario.Name) != null)
                    throw new Exception("El nombre de usuario ya existe. Por favor, elija otro nombre.");

                if (!ValidarPassword(usuario.Password))
                    throw new Exception("La contraseña debe tener al menos 8 caracteres y máximo 15, incluir al menos un número y una letra mayúscula.");

                usuario.PasswordE = BCrypt.Net.BCrypt.HashPassword(usuario.Password.Trim());

                USUARIO userDb = new USUARIO
                {
                    idUsuario = usuario.IdUser,
                    nombre = usuario.Name,
                    contraseña = usuario.PasswordE,
                    idRol = 1,
                    idEstado = 1,
                    apellido = usuario.Apellido,
                    correoElectronico = usuario.Correo,
                    numeroContacto = usuario.Numero
                };

                usuario.Respuesta = _userRepository.GuardarUsuario(userDb);
                usuario.Mensaje = usuario.Respuesta ? "Usuario registrado exitosamente." : "Error al registrar el usuario.";
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.Respuesta = false;
                usuario.Mensaje = ex.Message;
                return usuario;
            }
        }

        public UserDto LoginUser(UserDto userModel)
        {
            UserDto userResponse = _userRepository.BuscarUsuario(userModel.Name);
            if (userResponse != null && BCrypt.Net.BCrypt.Verify(userModel.Password, userResponse.PasswordE))
            {
                userResponse.Mensaje = "Successful Login";

                Correo gestorCorreo = new Correo();
                gestorCorreo.EnviarCorreo("valentinavelandiacastro2005@gmail.com",
                    "Registro exito / Reporte Semanal / ...", mensaje(userResponse.Name), true);
            }
            else
            {
                userResponse = new UserDto { Mensaje = "Error Login, Username or Password are Wrong" };
            }
            return userResponse;
        }

        private bool ValidarPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) &&
                   password.Length >= 8 &&
                   password.Length <= 15 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }

        private string mensaje(string name)
        {
            return "<html><body><h1> CRACKES </h1></br><p> Bienvenido(a) <b> " + name + " </b></p>" +
                   "<p> Notificación Automática. <b> No responder a este correo.</b></p><ol><li> Item 1 </li>" +
                   "<li> Item 2 </li><li> Item 3 </li></ol></body></html>";
        }
    }
}
