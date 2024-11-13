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

        // Constructor que recibe User_AdminRepository como parámetro
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