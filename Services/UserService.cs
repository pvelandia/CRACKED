using CRACKED.Dtos;
using CRACKED.Repositories;
using CRACKED.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Services
{
    public class UserService
    {
        //por ahora no
        public UserListDto ListarUsuarios()
        {
            UserListDto userList = new UserListDto();
            UserRepository userRepository = new UserRepository();
            try
            {
                userList = userRepository.ListarUsuarios();
                return userList;
            }
            catch (Exception e)
            {
                userList.Response = 0;
                userList.Message = e.InnerException.ToString();
                return userList;
            }
        }
        //validaciones true y asi
        public UserDto RegistroUsuarios(UserDto usuario)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                usuario.Respuesta = userRepository.RegistroUsuarios(usuario);
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                if (!usuario.Respuesta)
                    usuario.Mensaje = "Algo paso al resgistrar usuario";
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
            UserRepository userRepository = new UserRepository();
            
            UserDto userResponse = userRepository.inicioSesion(userModel);
            if (userResponse.IdUser != 0 && userResponse.IdUser !=null)
            {
                userResponse.Mensaje = "Successful Login";

                Correo gestorCorreo = new Correo();
                string destinatario = "valentinavelandiacastro2005@gmail.com";
                string asunto = "Registro exito / Reporte Semannal / ...";
                string mensaje = this.mensaje(userResponse.Name);
                gestorCorreo.EnviarCorreo(destinatario, asunto, mensaje, true);
            }
            else
            {
                userResponse.Mensaje = "Error Login, Username or Password are Wrong";
            }

            return userResponse;
        }

        public string mensaje(string name)
        {
            string mensaje = "<html>" +
                    "<body>" +
                        "<h1> CRACKES </h1>" +
                        "</br>" +
                        "<p> Bienvenido(a) <b> " + name + " </b></p>" +
                        "<p> Notificación Automatica. <b> No responder a este correo.</b></p>" +
                        "<ol>" +
                            "<li> Item 1 </li>" +
                            "<li> Item 2 </li>" +
                            "<li> Item 3 </li>" +
                        "</ol>" +
                    "</body>" +
                    "</html> ";

            return mensaje;
        }
    }
}