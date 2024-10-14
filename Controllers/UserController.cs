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

        [HttpPost]
        public ActionResult RegistroUsuarios(UserDto usuario)
        {
            UserService userSevice = new UserService();
            usuario= userSevice.RegistroUsuarios(usuario);

            if (usuario.Respuesta==true)
            {
                return View("Index");
            }
            else
            {
                return View("RegistroUsuarios");
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
            UserDto usuario = new UserDto();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult inicioSesion(UserDto usuario)
        {
            if (ModelState.IsValid)
            {
                bool esValido = _userService.AutenticarUsuario(usuario);

                if (esValido)
                {
                    // Aquí puedes establecer la sesión o redirigir a una página principal
                    // Session["Usuario"] = usuario.Name; // Por ejemplo
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Lógica para el inicio de sesión fallido
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                }
            }
            return View(usuario);
        }
    
public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Userr/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Userr/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Userr/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Userr/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Userr/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Userr/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
