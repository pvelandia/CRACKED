using CRACKED.Dtos;
using CRACKED.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CRACKED.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _carritoService;

        public CarritoController()
        {
            _carritoService = new CarritoService(); // Inicializamos el servicio
        }

        // Método para agregar un producto al carrito
        [HttpPost]
        public ActionResult AgregarProductoAlCarrito(int productoId, int cantidad, float precio)
        {
            // Obtener idCliente desde la sesión
            int idCliente = Convert.ToInt32(Session["IdCliente"]);

            // Obtener el ID del pedido desde la sesión
            int idPedido = ObtenerIdPedidoDeSesion();

            // Llamamos al servicio para agregar el producto al carrito
            _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente);

            // Redirigimos a la vista del carrito
            return RedirectToAction("VerCarrito");
        }

        // Método para obtener los productos del carrito de un cliente
        public ActionResult VerCarrito()
        {
            // Obtener idCliente desde la sesión
            int idCliente = Convert.ToInt32(Session["IdCliente"]);

            // Obtener el ID del pedido desde la sesión
            int idPedido = ObtenerIdPedidoDeSesion();

            // Llamamos al servicio para obtener los productos del carrito
            var productosEnCarrito = _carritoService.ObtenerProductosEnCarrito(idCliente, idPedido);

            // Retornamos a la vista
            return View(productosEnCarrito);
        }

        // Método para eliminar un producto del carrito
        [HttpPost]
        public ActionResult EliminarProductoDelCarrito(int productoId)
        {
            // Obtener idCliente desde la sesión
            int idCliente = Convert.ToInt32(Session["IdCliente"]);

            // Obtener el ID del pedido desde la sesión
            int idPedido = ObtenerIdPedidoDeSesion();

            // Llamamos al servicio para eliminar el producto
            _carritoService.EliminarProductoDelCarrito(productoId, idCliente, idPedido);

            // Redirigimos al carrito
            return RedirectToAction("VerCarrito");
        }

        // Método privado para obtener el idPedido de la sesión
        private int ObtenerIdPedidoDeSesion()
        {
            // Aquí se puede obtener el idPedido desde la sesión
            return Convert.ToInt32(Session["IdPedido"]);
        }
    }
}
