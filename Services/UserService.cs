using CRACKED.Dtos;
using CRACKED.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Services
{
    public class UserService
    {
        public UserDto RegistroUsuarios(UserDto usuario)
        {
            UserRepository userRepository = new UserRepository();
            usuario.Respuesta = userRepository.RegistroUsuarios(usuario);
            if (!usuario.Respuesta)
                usuario.Mensaje="Algo paso al resgistrar usuario";
            return usuario;
        }


            private readonly UserRepository _userRepository;

            public UserService()
            {
                _userRepository = new UserRepository();
            }

            public bool AutenticarUsuario(UserDto usuario)
            {
                return _userRepository.ValidarUsuario(usuario);
            }
        }
    }
