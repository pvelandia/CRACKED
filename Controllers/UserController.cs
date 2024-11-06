using CRACKED.Dtos;
using CRACKED.Repositories;
using CRACKED.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Controllers
{ //vistas trae clases
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }
        public ActionResult Cookiecakes()
        {
            return View();
        }

        public ActionResult Detalle_Cookiecakes()
        {
            return View();
        }
        public ActionResult Galletas()
        {
            return View();
        }
        public ActionResult Detalle_Galletas()
        {
            return View();
        }
        public ActionResult RegistroUsuarios()
        {
            UserDto usuario = new UserDto();
            return View(usuario);
        }
        public ActionResult ListaUsuarios()
        {
            UserService userService = new UserService();
            UserListDto usersList = userService.ListarUsuarios();
            return View(usersList);
        }

        [HttpPost]
        public ActionResult RegistroUsuarios(UserDto usuario)
        {
            try
            {
                UserService userSevice = new UserService();
                usuario = userSevice.RegistroUsuarios(usuario);

                if (usuario.Respuesta == true)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.ErrorMessage = usuario.Mensaje;
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                usuario.Mensaje = ex.Message;
                return View(usuario);
            }

        }

        [HttpGet]

        public ActionResult inicioSesion()
        {
            UserDto user = new UserDto();

            return View(user);
        }

      
        [HttpPost]
        public ActionResult inicioSesion(UserDto user)
        {
          
                UserService userService = new UserService();
                UserDto userLogin = userService.LoginUser(user);

                if (userLogin.IdUser != 0)
                {
                    Session["UserLogged"] = userLogin;

                    // Obtener el IdRol del usuario autenticado
                    int idRol = userService.ObtenerIdRol(userLogin.IdUser);

                    // Seleccionar el layout basado en el rol
                    if (idRol == 1)
                    {
                        // Asignar layout para el cliente
                        ViewBag.Layout = "_Layout.cshtml";
                    }
                    else if (idRol == 2)
                    {
                        // Asignar layout para el administrador
                        ViewBag.Layout = "Error.cshtml";
                    }
                    else if (idRol == 3)
                    {
                        // Asignar layout para el domiciliario
                        ViewBag.Layout = "_LayoutDomiciliario";
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Credenciales incorrectas.";
                    return View();
                }
            }
        [HttpPost]
        public ActionResult Cookiecakes(UserDto model, int quantity, string selectionInput)
        {
            
            if (model == null)
            {
                return ViewBag.ErrorMessage("El modelo es nulo.");
            }

            // Aquí iría tu lógica para manejar el carrito

            if (quantity <= 0)
            {
                return ViewBag.ErrorMessage("La cantidad debe ser mayor a cero.");
            }

            // Supongamos que todo está bien y quieres redirigir
            // Por ejemplo, después de agregar al carrito
            return RedirectToAction("Index");
        }

    }
}

