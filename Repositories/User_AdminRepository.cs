using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Repositories
{

    public class User_AdminRepository
    {
        public List<USUARIO> ObtenerUsuariosFiltrados(string searchValue, string filterValue)
        {
            using (var db = new CRACKEDEntities36())
            {
                var query = db.USUARIOs.AsQueryable();

                // Aplicar filtro de búsqueda si existe
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(u => u.nombre.Contains(searchValue) ||
                                             u.apellido.Contains(searchValue) ||
                                             u.correoElectronico.Contains(searchValue));
                }

                // Aplicar filtro de rol y estado
                if (!string.IsNullOrEmpty(filterValue))
                {
                    switch (filterValue)
                    {
                        case "administrador":
                            query = query.Where(u => u.idRol == 2);
                            break;
                        case "domiciliario":
                            query = query.Where(u => u.idRol == 3);
                            break;
                        case "cliente":
                            query = query.Where(u => u.idRol == 1);
                            break;
                        case "activo":
                            query = query.Where(u => u.idEstado == 1);
                            break;
                        case "inactivo":
                            query = query.Where(u => u.idEstado == 0);
                            break;
                    }
                }

                return query.ToList();
            }
        }
        public void ActualizarUsuario(USUARIO usuario)
        {
            using (var db = new CRACKEDEntities36())
            {
                var usuarioExistente = db.USUARIOs.Find(usuario.idUsuario);
                if (usuarioExistente != null)
                {
                    usuarioExistente.nombre = usuario.nombre;
                    usuarioExistente.apellido = usuario.apellido;
                    usuarioExistente.numeroContacto = usuario.numeroContacto;
                    usuarioExistente.correoElectronico = usuario.correoElectronico;
                    usuarioExistente.idEstado = usuario.idEstado;
                    usuarioExistente.idRol = usuario.idRol;

                    db.SaveChanges();
                }
            }
        }



        public void EliminarUsuario(int idUsuario)
            {
                using (var db = new CRACKEDEntities36())
                {
                    var usuario = db.USUARIOs.FirstOrDefault(u => u.idUsuario == idUsuario);
                    if (usuario != null)
                    {
                        db.USUARIOs.Remove(usuario);
                        db.SaveChanges();
                    }
                }
            }
        public UserDto ObtenerUsuarioPorId(int idUsuario)
        {
            using (var db = new CRACKEDEntities36())
            {
                var usuario = db.USUARIOs.Find(idUsuario);
                if (usuario != null)
                {
                    return new UserDto
                    {
                        IdUser = usuario.idUsuario,
                        Name = usuario.nombre,
                        Apellido = usuario.apellido,
                        Numero = usuario.numeroContacto,
                        Correo = usuario.correoElectronico,
                        IdEstado = usuario.idEstado,
                        IdRol = usuario.idRol
                    };
                }
                return null;
            }
        }


    }
}

