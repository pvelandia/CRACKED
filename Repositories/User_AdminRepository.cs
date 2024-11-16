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
        private readonly CRACKEDEntities41 _context;
        public User_AdminRepository(CRACKEDEntities41 context)
        {
            _context = context;
        }

        public List<USUARIO> ObtenerUsuariosFiltrados(string searchValue, string filterValue)
        {
            using (var db = new CRACKEDEntities41())
            {
                var query = db.USUARIOs.AsQueryable();

                // filtro de búsqueda si existe
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(u => u.nombre.Contains(searchValue) ||
                                             u.apellido.Contains(searchValue) ||
                                             u.correoElectronico.Contains(searchValue));
                }

                // filtro de rol y estado
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
                            query = query.Where(u => u.idEstado == 2);
                            break;
                    }
                }

                return query.ToList();
            }
        }
        public void ActualizarUsuario(USUARIO usuario)
        {
            using (var db = new CRACKEDEntities41())
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

        //actualiza el rol y el estado del usuario
        public void ActualizarEstadoUsuario(int idUsuario, int idEstado,int idRol)
        {
            var usuario = _context.USUARIOs.FirstOrDefault(u => u.idUsuario == idUsuario);
            if (usuario != null)
            {
                usuario.idEstado = idEstado;// Actualizamos el estado del usuario
                usuario.idRol = idRol;// Actualizamos el rol del usuario
                _context.SaveChanges(); 
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }
            public void EliminarUsuario(int idUsuario)
            {
                using (var db = new CRACKEDEntities41())
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
            using (var db = new CRACKEDEntities41())
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
                        Estado=usuario.idEstado,
                        Estadonombre = usuario.ESTADO.nombre,
                        IdRol = usuario.idRol,
                        Rolnombre=usuario.ROL.nombre
                    };
                }
                return null;
            }
        }


    }
}

