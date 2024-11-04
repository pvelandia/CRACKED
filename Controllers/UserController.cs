using CRACKED.Dtos;
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
                return RedirectToAction("Index","Home");
            }

            return View(userLogin);
        }
    }
}

