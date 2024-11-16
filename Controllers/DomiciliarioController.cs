using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRACKED.Dtos;
using CRACKED.Services;
using CRACKED.Repositories;
using CRACKED.Models;

namespace CRACKED.Controllers
{
    public class DomiciliarioController : Controller
    {
        private readonly PedidoService _pedidoService;
        private readonly RegistroProductoService _registroProductoService;

        public DomiciliarioController()
        {
            var context = new CRACKEDEntities41();

            // Crear instancias de los repositorios
            var pedidoRepository = new PedidoRepository(context);
            var registroProductoRepository = new RegistroProductoRepository(context);

            // Crear las instancias de los servicios pasando los repositorios
            _pedidoService = new PedidoService(pedidoRepository);
            _registroProductoService = new RegistroProductoService(registroProductoRepository);
        }
        
        public ActionResult Pedidos()
        {
            List<PedidoAdminDTO> pedidos = _pedidoService.ObtenerPedidos();
            return View(pedidos);
        }
        public ActionResult DetallesPedidos(int id)
        {
          
                PedidoAdminDTO pedido = _pedidoService.ObtenerPedidoPorId(id);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(pedido);
           
        }
        public ActionResult ProductosPedidos(int id)
        {
                // Obtiene los productos del pedido desde la tabla registro_productos
                var productos = _registroProductoService.ObtenerProductosDePedido(id);

                // Pasar los productos a la vista
                return View(productos);
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
                // Llama al servicio para actualizar el estado del pedido
                _pedidoService.ActualizarEstadoPedido(idPedido, idEstado);

                // Devuelve una respuesta de éxito
                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (Exception ex)
            {
                // Devuelve una respuesta de error
                return Json(new { success = false, message = $"Ocurrió un error: {ex.Message}" });
            }
        }
    }
}