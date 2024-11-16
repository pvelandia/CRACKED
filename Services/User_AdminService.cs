using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Services
{
    public class User_AdminService
    {
        private readonly User_AdminRepository _userAdminRepository;

     
        public User_AdminService(User_AdminRepository usuarioRepository)
        {
            _userAdminRepository = usuarioRepository;
        }

        public List<USUARIO> ObtenerUsuariosFiltrados(string searchValue, string filterValue)
        {
            return _userAdminRepository.ObtenerUsuariosFiltrados(searchValue, filterValue);
        }
        public void ActualizarUsuario(USUARIO usuario)
        {
            _userAdminRepository.ActualizarUsuario(usuario);
        }


        public void ActualizarEstadoUsuario(int idUsuario, int idEstado, int idRol)
        {
            var usuario = _userAdminRepository.ObtenerUsuarioPorId(idUsuario);
            if (usuario != null)
            {
                usuario.Estado = idEstado; // Actualizamos el estado del usuario
                _userAdminRepository.ActualizarEstadoUsuario(idUsuario,idEstado, idRol); // Guardamos los cambios
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }

        public UserDto ObtenerUsuarioPorId(int idUsuario)
        {
            return _userAdminRepository.ObtenerUsuarioPorId(idUsuario);
        }
        public void EliminarUsuario(int idUsuario)
        {
            _userAdminRepository.EliminarUsuario(idUsuario);
        }

    }
}