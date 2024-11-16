using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using CRACKED.Services;
using Rotativa;
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
        private readonly ProductService _productService;
        private readonly PedidoService _pedidoService;
        private readonly ReporteService _reporteService;
        private readonly RegistroProductoService _registroProductoService;
        public AdminController()
        {
            var context = new CRACKEDEntities39();
            var pedidoRepository = new PedidoRepository(context);
            var registroProductoRepository = new RegistroProductoRepository(context);
            _usuarioService = new User_AdminService(new User_AdminRepository(context));
            _productService = new ProductService();
            _pedidoService = new PedidoService(pedidoRepository);
            _reporteService = new ReporteService();
            _registroProductoService = new RegistroProductoService(registroProductoRepository);

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
                IdEstado = usuario.Estado,
                IdRol = usuario.IdRol
            };
            return View(viewModel);
        }

        //// Acción para actualizar un usuario
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ActualizarUsuario(UsuarioViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var usuarioDto = new USUARIO
        //        {
        //            idUsuario = model.IdUsuario,
        //            nombre = model.Nombre,
        //            apellido = model.Apellido,
        //          numeroContacto= model.NumeroContacto,
        //            correoElectronico = model.CorreoElectronico,
        //            idEstado = model.IdEstado,
        //            idRol = model.IdRol
        //        };

        //        _usuarioService.ActualizarUsuario(usuarioDto);
        //        return RedirectToAction("Index"); // Redirige a la lista de usuarios o al perfil del usuario
        //    }

        //    return View(model); // Si hay errores de validación, vuelve a la vista con los mensajes de error
        //}

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
                IdEstado = usuario.Estado,
                IdRol = usuario.IdRol
            };
            return View(viewModel);

        }


        public ActionResult Pedidos()
        {

            List<PedidoAdminDTO> pedidos = _pedidoService.ObtenerPedidos();
            return View(pedidos);
        }
        public ActionResult Productos()
        {
            ProductListDto products = _productService.ObtenerProductosAdmin();
            return View(products);
        }
        public ActionResult Reportes()
        {
            return View();
        }

        
        public ActionResult EditarProducto(int id)
        {
            var producto = _productService.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return HttpNotFound();  
            }

            return View(producto); 
        }

        
        //[HttpPost]
        //public ActionResult Editar(ProductDto productDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool actualizado = _productService.ActualizarProducto(productDto);
        //        if (actualizado)
        //        {
        //            return RedirectToAction("Index");  // Redirige a la lista de productos
        //        }
        //        else
        //        {
        //            ViewBag.Message="No se pudo actualizar el producto.";
        //        }
        //    }

        //    return View(productDto);  // Si hay un error, vuelve a mostrar el formulario
        //}
    
    public ActionResult AgregarProducto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarProducto(ProductDto productDto)
        {
            try
            {
                if (_productService.CrearProducto(productDto))
                {
                    ViewBag.Message = "¡Producto agregado exitosamente!";
                }
                else
                {
                    ViewBag.Error = "No se pudo agregar el producto.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al guardar el producto: {ex.Message}";
            }

            return View();
        }


        [HttpPost]
        public ActionResult Eliminar(int idProducto)
        {
            try
            {
                // Llamar al servicio para eliminar el producto
                var resultado = _productService.DeleteProduct(idProducto);

                if (resultado)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "No se pudo eliminar el producto." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        public ActionResult DetallePedido(int id)
        {
            PedidoAdminDTO pedido = _pedidoService.ObtenerPedidoPorId(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }
        public ActionResult ProductosPedido(int id)
        {
            // Obtiene los productos del pedido desde la tabla registro_productos
            var productos = _registroProductoService.ObtenerProductosDePedido(id);

            // Pasar los productos a la vista
            return View(productos);
        }

        [HttpPost]

        public ActionResult User_AdminReportPrint(string tipoReporte)
        {
            // Verifica el tipo de reporte y genera los datos correspondientes
            var report = new ReportDto2
            {
                List = tipoReporte == "Pedidos" ?
                    _reporteService.ObtenerReportePedidos() :
                    _reporteService.ObtenerReporteUsuarios(),
                GeneradoPor = "UserLogado", // Nombre del usuario logueado
                Fecha = DateTime.Now.ToString(),
                TipoReporte = tipoReporte  // Pasa el tipo de reporte
            };

            // Usa la misma vista para mostrar el reporte o generar el PDF
            return new ViewAsPdf("User_AdminReportPrint", report)
            {
                FileName = "Reporte-" + tipoReporte + "-" + Guid.NewGuid() + ".pdf"
            };
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Obtener el usuario de la sesión
            var userLogin = Session["UserLogged"] as UserDto;

            // Si el usuario está autenticado, pasa el objeto completo a ViewBag
            if (userLogin != null)
            {
                ViewBag.User = userLogin;
         
            }

            base.OnActionExecuting(filterContext);
        }
       
        [HttpPost]
        public ActionResult GuardarEstado(int idPedido, int idEstado)
        {
            try
            {
                // Llamamos al servicio para actualizar el estado del pedido
                _pedidoService.ActualizarEstadoPedido(idPedido, idEstado);

                // Mostrar mensaje de éxito
                TempData["SuccessMessage"] = "Estado actualizado correctamente.";

                // Redirigir a la vista de detalle del pedido
                return RedirectToAction("Pedidos");
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar mensaje de error
                TempData["ErrorMessage"] = $"Ocurrió un error: {ex.Message}";
                return RedirectToAction("DetallePedido", new { id = idPedido });
            }
        }
        public ActionResult GuardarEstadoUsuario(int idUsuario, int idEstado, int idRol)
        {
            try
            {
                // Llamamos al servicio para actualizar el estado del usuario
                _usuarioService.ActualizarEstadoUsuario(idUsuario, idEstado,idRol);

                // Mostrar mensaje de éxito
                TempData["SuccessMessage"] = "Estado del usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar mensaje de error
                TempData["ErrorMessage"] = $"Ocurrió un error: {ex.Message}";
            }

            // Redirigir a la vista de lista de usuarios
            return RedirectToAction("InfoUsuarios");
        }
    }

}