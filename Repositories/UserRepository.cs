using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public bool ValidarUsuario(UserDto usuario)
        {
            try
            {
                using (var db = new CRACKEDEntities4())
                {
                    // Busca un usuario que coincida con el nombre y la contraseña
                    var userDb = db.USUARIOs
                        .FirstOrDefault(u => u.nombre == usuario.Name && u.contraseña == usuario.Password);

                    // Verifica si se encontró el usuario
                    return userDb != null;
                }
            }
            catch (Exception ex)
            {
                // Registra el error o lanza una excepción
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}