using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CRACKED.Dtos.UserListDto;

namespace CRACKED.Repositories
{
    public class UserRepository
    {
        public bool RegistroUsuarios(UserDto usuario)
        {
            UserDto respuesta = new UserDto();
            try
            {
                //ACCESO DE DATOS
                using (var db = new CRACKEDEntities4())
                {
                    USUARIO userDb = new USUARIO();

                    //userDb.idUsuario = usuario.IdUsuario;
                    userDb.nombre = usuario.Name;
                    userDb.contraseña = usuario.Password;
                    //userDb.idRol = usuario.IdRol;
                    //userDb.idEstado = usuario.IdEstado;
                    //userDb.apellido = usuario.Apellido;
                    //userDb.correoElectronico = usuario.Correo;
                    //userDb.numeroContacto = usuario.Numero;

                    db.USUARIOs.Add(userDb);
                    db.SaveChanges();


                    //respuesta.Respuesta = true;
                    //respuesta.Mensaje = "Usuario Creado Exitosamente";
                    //if (userDb.idUsuario!=0)
                    //{
                    //    usuario.Id = userDb.idUsuario;
                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Aquí puedes registrar el error o lanzarlo
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return false; // O maneja la excepción como sea necesario
            }
        }




        //}
        //catch (Exception ex)
        //{
        //    //        //respuesta.Respuesta = false;
        //    //        //respuesta.Mensaje = ex.Message;
        //            return false;
        //}

        //public bool ValidarUsuario(UserDto usuario)
        //{
        //    try
        //    {
        //        using (var db = new CRACKEDEntities4())
        //        {
        //            // Busca un usuario que coincida con el nombre y la contraseña
        //            var userDb = db.USUARIOs
        //                .FirstOrDefault(u => u.nombre == usuario.Name && u.contraseña == usuario.Password);

        //            // Verifica si se encontró el usuario
        //            return userDb != null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra el error o lanza una excepción
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return false;
        //    }
        //}
        public UserListDto ListarUsuarios()
        {
            UserListDto userListDto = new UserListDto();

            try
            {
                using (var db = new CRACKEDEntities4())
                {
                    // Obtenemos la lista de usuarios desde la base de datos
                    var usuarios = db.USUARIOs.Select(u => new UserDto
                    {
                        IdUser = u.idUsuario,
                        IdRol = (int)u.idRol,
                        IdEstado = (int)u.idEstado,
                        Name = u.nombre,

                    }).ToList();

                    // Asignamos la lista de UserDto a la propiedad 'Usuarios' de UserListDto
                    userListDto.Users = usuarios;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar usuarios: " + ex.Message);
            }

            return userListDto;
        }


        public bool BuscarUsuario(string username)
        {
            bool result = false;

            try
            {
                using (var db = new CRACKEDEntities4())
                {
                    // Busca en la base de datos si hay algún usuario con el nombre indicado
                    var userDb = db.USUARIOs.FirstOrDefault(u => u.nombre == username);

                    // Si se encuentra un usuario con ese nombre, devuelve true
                    if (userDb != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que ocurra durante la búsqueda
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        public UserDto inicioSesion(UserDto user)
        {
            UserDto result = null; // Inicializa el resultado como null

            try
            {
                using (var db = new CRACKEDEntities4())
                {
                    // Busca al usuario en la base de datos usando el nombre
                    var userDb = db.USUARIOs.FirstOrDefault(u => u.nombre == user.Name);

                    if (userDb != null)
                    {
                        Console.WriteLine("Usuario encontrado.");

                        // Asegúrate de que la contraseña ingresada esté encriptada antes de la comparación
                        //string encryptedPassword = Encriptar.GetSHA256(user.Password);

                        // Si el usuario existe, compara las contraseñas
                        if (userDb.contraseña == user.Password)
                        {
                            // Si la contraseña es correcta, crea un nuevo UserDto
                            result = new UserDto
                            {
                                IdUser = userDb.idUsuario,
                                Name = userDb.nombre,
                                Password =userDb.contraseña,
                                // Puedes agregar más propiedades si es necesario
                            };
                            Console.WriteLine("Inicio de sesión exitoso.");
                        }
                        else
                        {
                            Console.WriteLine("Contraseña incorrecta.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("El usuario no existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                Console.WriteLine("Error en el inicio de sesión: " + ex.Message);
            }

            return result; // Retorna el UserDto si el inicio fue exitoso o null si falló
        }

    }
}