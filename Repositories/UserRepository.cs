using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
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
                using (var db = new CRACKEDEntities10())
                {
                 
                    if (usuario.Password != usuario.ConfirmPassword)
                    {
                        throw new Exception("Las contraseñas no coinciden. Por favor, verifícalas.");

                    }
                 

                    if (BuscarUsuario(usuario.Name))
                    {
                        throw new Exception("El nombre de usuario ya existe. Por favor, elija otro nombre.");
                    }

                    //metodo mas abajo valida
                    if (!ValidarPassword(usuario.Password))
                    {
                        throw new Exception("La contraseña debe tener al menos 8 caracteres y máximo 15, incluir al menos un número y una letra mayúscula.");
                    }

                    USUARIO userDb = new USUARIO();

                    userDb.idUsuario = usuario.IdUser;
                    userDb.nombre = usuario.Name;
                    usuario.PasswordE = BCrypt.Net.BCrypt.HashPassword(usuario.Password.Trim());
                    userDb.contraseña = usuario.PasswordE;

                    userDb.idRol = usuario.IdRol;
                    userDb.idEstado = usuario.IdEstado;
                    userDb.apellido = usuario.Apellido;
                    userDb.correoElectronico = usuario.Correo;
                    userDb.numeroContacto = usuario.Numero;

                    db.USUARIOs.Add(userDb);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return false; 
            }
        }

        private bool ValidarPassword(string password)
        {
            // La contraseña debe tener entre 8 y 15 caracteres, al menos una letra mayúscula y un número
            if (password.Length < 8 || password.Length > 15)
                return false;

            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneNumero = password.Any(char.IsDigit);

            return tieneMayuscula && tieneNumero;
        }

        //lo usaremos despues  depronto usar tabla usuarios
        public UserListDto ListarUsuarios()
        {
            UserListDto userListDto = new UserListDto();

            try
            {
                using (var db = new CRACKEDEntities10())
                {
                    
                    var usuarios = db.USUARIOs.Select(u => new UserDto
                    {
                        IdUser = u.idUsuario,
                        IdRol = (int)u.idRol,
                        IdEstado = (int)u.idEstado,
                        Name = u.nombre,

                    }).ToList();

                    userListDto.Users = usuarios;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar usuarios: " + ex.Message);
            }

            return userListDto;
        }

        //se usa en el registro e inicio
        public bool BuscarUsuario(string username)
        {
            bool result = false;

            try
            {
                using (var db = new CRACKEDEntities10())
                {
                
                    var userDb = db.USUARIOs.FirstOrDefault(u => u.nombre == username);

                    if (userDb != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public UserDto inicioSesion(UserDto user)
        {
            UserDto result = new UserDto(); 

            try
            {
                using (var db = new CRACKEDEntities10())
                {
                    if (BuscarUsuario(user.Name))
                    {
                        var userDb = db.USUARIOs.FirstOrDefault(u => u.nombre == user.Name);
                        //user.PasswordE = BCrypt.Net.BCrypt.HashPassword(user.Password.Trim());

                        Console.WriteLine("Usuario encontrado.");
                        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, userDb.contraseña);
                        if (isPasswordValid == true) // Si la contraseña es correcta, crea un nuevo UserDto
                        {
                            result = new UserDto
                                {
                                    IdUser = userDb.idUsuario,
                                    Name = userDb.nombre,
                                    Password = userDb.contraseña,
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
                Console.WriteLine("Error en el inicio de sesión: " + ex.Message);
            }

            return result; // lo que retorna es el UserDto si el inicio fue exitoso o null si falló
        }

        
    }
}