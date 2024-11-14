using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Linq;

namespace CRACKED.Repositories
{
    public class UserRepository
    {
        public bool GuardarUsuario(USUARIO userDb)
        {
            try
            {
                using (var db = new CRACKEDEntities35())
                {
                    db.USUARIOs.Add(userDb);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserDto BuscarUsuario(string username)
        {
            try
            {
                using (var db = new CRACKEDEntities35())
                {
                    var userDb = db.USUARIOs.FirstOrDefault(u => u.nombre == username);
                    if (userDb != null)
                    {
                        return new UserDto
                        {
                            IdUser = userDb.idUsuario,
                            Name = userDb.nombre,
                            PasswordE = userDb.contraseña,
                            IdRol = (int)userDb.idRol,
                            IdEstado = (int)userDb.idEstado
                        };
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de error
            }
            return null;
        }

        public UserListDto ListarUsuarios()
        {
            UserListDto userListDto = new UserListDto();
            try
            {
                using (var db = new CRACKEDEntities35())
                {
                    userListDto.Users = db.USUARIOs.Select(u => new UserDto
                    {
                        IdUser = u.idUsuario,
                        IdRol = (int)u.idRol,
                        IdEstado = (int)u.idEstado,
                        Name = u.nombre
                    }).ToList();
                }
            }
            catch (Exception)
            {
                // Manejo de error
            }
            return userListDto;
        }
    }
}
