using System;
using System.Web.Mvc;
using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using CRACKED.Services;

namespace CRACKED.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _carritoService;

        // Constructor sin parámetros
        public CarritoController()
        {
            // Aquí se pasa tanto CarritoRepository como ProductRepository al CarritoService
            _carritoService = new CarritoService(
                new CarritoRepository(new CRACKEDEntities31()),
                new ProductRepository()
            );
        }

        // Constructor con parámetros (necesario cuando tienes IoC configurado)
        public CarritoController(CarritoService carritoService)
        {
            _carritoService = carritoService;
        }

      
        public ActionResult DatosEntrega()
        {
            return View();
        }

        // Método POST para agregar un producto al carrito
        [HttpPost]
        public ActionResult AgregarProductoAlCarrito(int productoId, int cantidad, float precio, int porcion=1)
        {
            try
            {
                // Validación: asegurar que se haya seleccionado solo una porción y no ambas
                // Validación: asegurar que se haya seleccionado una porción válida (4 o 8)
                

                var user = Session["UserLogged"] as UserDto;
                if (user != null)
                {
                    
                    float precioTotal = precio * cantidad;
                    precio = precioTotal;

                    int idCliente = user.IdUser;
                    int idPedido = ObtenerIdPedido(idCliente); // Método que debe retornar el idPedido asociado
                    if (porcion == 8)
                    {
                        precio = precio * 2;
                    }

                    // Llamamos al servicio para agregar el producto al carrito, pasando la porción seleccionada
                    _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente, porcion);
                }
                else
                {
                    return RedirectToAction("inicioSesion", "User");
                }

                return RedirectToAction("Carrito");
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de excepciones, si el stock es insuficiente o algo salió mal
                ViewBag.ErrorMessage = "El stock es insuficiente para el producto que seleccionaste.";
                return RedirectToAction("Productos", "User");
            }
        }
        public ActionResult Carrito()
        {
            // Obtener el objeto UserDto desde la sesión
            var user = Session["UserLogged"] as UserDto;

            // Verificar si el objeto UserDto es null
            if (user == null)
            {
                // Manejar el caso en que el usuario no esté logueado
                return RedirectToAction("inicioSesion", "User");
            }

            // Usar el idCliente desde el objeto UserDto
            int idCliente = user.IdUser;

            // Ahora puedes pasar el idCliente al servicio para obtener los datos del carrito
            var carritoItems = _carritoService.ObtenerCarritoPorCliente(idCliente);
            return View(carritoItems);
        }
        [HttpPost]
        public ActionResult EliminarProducto(int idPedidoProducto)
        {
            bool resultado = _carritoService.EliminarProducto(idPedidoProducto); // Llamamos al servicio para eliminar

            if (resultado)
            {
                return Json(new { success = true }); // Retorna un JSON indicando que la eliminación fue exitosa
            }
            else
            {
                return Json(new { success = false }); // Retorna un JSON indicando que hubo un error
            }

        }

            public ActionResult CrearPedido(int idCliente)
        {
            var pedidoDto = new PedidoDto
            {
                IdCliente = idCliente,
                IdDomiciliario = 1,
                IdCiudad = 1,
                IdMetodoPago = 1,
                IdEstado = 1,
                FechaEntrega = DateTime.Now,
                FechaVenta = DateTime.Now,
                Direccion = "cra 6 #14-53",
                ValorTotal = 4567,
                Iva = 19,
                Estado = "En progreso"
            };

            // Supongamos que no se han pasado los productos
            int productoId = 0;
            int cantidad = 0;
            float precio = 0;
            int idPedido = 0;
            int porcion = 0;

            // Agregar el producto al carrito
            _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente, porcion);

            // Redirige al inicio
            return RedirectToAction("Index", "Home");
        }

        private int ObtenerIdPedido(int idCliente)
        {
            // Método de ejemplo que devuelve un idPedido
            return 1; // Aquí deberías obtener el idPedido real asociado con el cliente
        }
    }
}
