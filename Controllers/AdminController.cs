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
        public ActionResult DatosUsuario()
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