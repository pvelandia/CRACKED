using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using CRACKED.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Controllers
{
    public class AdminController : Controller
    {
        private User_AdminService _usuarioService;
        public AdminController()
        {
            _usuarioService = new User_AdminService(new User_AdminRepository());
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InfoUsuarios()
        {
            var usuarios = _usuarioService.ObtenerUsuariosFiltrados("", "");
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult InfoUsuariosFiltered(string search, string filter)
        {
            var usuariosFiltrados = _usuarioService.ObtenerUsuariosFiltrados(search, filter);
            return PartialView("_UsuariosTablePartial", usuariosFiltrados);
        }
        public ActionResult EditarUsuario(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);
            var viewModel = new UsuarioViewModel
            {
                IdUsuario = usuario.IdUser,
                Nombre = usuario.Name,
                Apellido = usuario.Apellido,
                NumeroContacto = usuario.Numero,
                CorreoElectronico = usuario.Correo,
                IdEstado = usuario.IdEstado,
                IdRol = usuario.IdRol
            };
            return View(viewModel);
        }

        // Acción para actualizar un usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarUsuario(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioDto = new USUARIO
                {
                    idUsuario = model.IdUsuario,
                    nombre = model.Nombre,
                    apellido = model.Apellido,
                  numeroContacto= model.NumeroContacto,
                    correoElectronico = model.CorreoElectronico,
                    idEstado = model.IdEstado,
                    idRol = model.IdRol
                };

                _usuarioService.ActualizarUsuario(usuarioDto);
                return RedirectToAction("Index"); // Redirige a la lista de usuarios o al perfil del usuario
            }

            return View(model); // Si hay errores de validación, vuelve a la vista con los mensajes de error
        }

        // Acción para eliminar un usuario (usando AJAX)
        [HttpPost]
        public JsonResult EliminarUsuario(int idUsuario)
        {
            try
            {
                _usuarioService.EliminarUsuario(idUsuario);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public ActionResult DatosUsuario(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);
            var viewModel = new UsuarioViewModel
            {
                IdUsuario = usuario.IdUser,
                Nombre = usuario.Name,
                Apellido = usuario.Apellido,
                NumeroContacto = usuario.Numero,
                CorreoElectronico = usuario.Correo,
                IdEstado = usuario.IdEstado,
                IdRol = usuario.IdRol
            };
            return View(viewModel);

        }


        public ActionResult Pedidos()
        {
            return View();
        }
        public ActionResult Productos()
        {
            return View();
        }
        public ActionResult Reportes()
        {
            return View();
        }
      
        public ActionResult AgregarProducto()
        {
            return View();
        }
        public ActionResult DetallePedido()
        {
            return View();
        }
        public ActionResult ProductosPedido()
        {
            return View();
        }
    }
}