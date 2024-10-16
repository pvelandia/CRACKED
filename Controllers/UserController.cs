using CRACKED.Dtos;
using CRACKED.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Controllers
{
    public class UserController : Controller
    {
        // GET: Userr
        public ActionResult Index()
        {
            return View();
        }

        // GET: Userr/Details/5

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
            UserService userSevice = new UserService();
            usuario = userSevice.RegistroUsuarios(usuario);

            if (usuario.Respuesta == true)
            {
                return View("Index");
            }
            else
            {
                return View(usuario);
            }

        }



        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]

        public ActionResult inicioSesion()
        {
            UserDto user = new UserDto();
            return View(user);
        }

        //POST Login
        [HttpPost]
        public ActionResult inicioSesion(UserDto user)
        {
            UserService userService = new UserService();
            UserDto userLogin = userService.LoginUser(user);

            if (userLogin.IdUser != 0)
            {
                Session["UserLogged"] = userLogin;
                return RedirectToAction("Index");
            }

            return View(userLogin);
        }
    }
}