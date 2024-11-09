using System;
using System.Web.Mvc;
using CRACKED.Dtos;
using CRACKED.Repositories;
using CRACKED.Services;

namespace CRACKED.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _carritoService;

        // Constructor con parámetros
        public CarritoController(CarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        // Constructor sin parámetros
        //public CarritoController() : this(new CarritoService(new CarritoRepository(new Models.CRACKEDEntities25()))
        //{
        //    // Puedes inicializar los servicios aquí si no se está utilizando un contenedor de DI
        //}
        // Método GET para mostrar el carrito (solo se utiliza [HttpGet] implícitamente)
        public ActionResult Carrito()
        {
            // Aquí va la lógica para mostrar los productos en el carrito
            return View();
        }
        public ActionResult DatosEntrega()
        {
            
            return View();
        }
        // Método POST para agregar un producto al carrito
        [HttpPost]
        public ActionResult AgregarProductoAlCarrito(int productoId, int cantidad, float precio)
        {
            // Obtener el Id del cliente desde la sesión
            var user = Session["UserLogged"] as UserDto;

            if (user != null)
            {
                // Accede a la propiedad IdUser dentro de UserDto
                int idCliente = user.IdUser;

                //int idCliente = Convert.ToInt32(Session["UserLogged"]);

                // Asumiendo que 'idPedido' se maneja de otra manera, si ya existe en la sesión o base de datos
                int idPedido = ObtenerIdPedido(idCliente); // Método que debe retornar el idPedido asociado

                // Llamada al servicio para agregar el producto al carrito
                _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente);
            }
            else
            {
                // Manejo de error si el objeto en la sesión no es del tipo esperado
                // Ejemplo: redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Login", "Account");
            }

                // Redirige a la vista del carrito después de agregar el producto
                return RedirectToAction("Carrito");
        }
        public ActionResult CrearPedido(int idCliente)
        {
            // Crear el DTO con los datos necesarios
            var pedidoDto = new PedidoDto
            {
                IdCliente = idCliente,
                IdDomiciliario = 1,
                IdCiudad = 1,
                IdMetodoPago = 1,
                IdEstado = 1,
                FechaEntrega = DateTime.Now,  // Arreglar
                FechaVenta = DateTime.Now,  // Usamos la fecha actual para la venta
                Direccion = "cra 6 #14-53",
                ValorTotal = 4567,  //Arreglar
                Iva = 19,  // Asegúrate de tener este valor
                Estado = "En progreso"  // O cualquier otro estado que corresponda
            };

            int productoId = 0;
            int cantidad = 0;
            float precio = 0;
            int idPedido = 0;
            // Llamar al servicio para crear el pedido
            _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente);

            // Redirigir o retornar alguna vista
            return RedirectToAction("Index", "Home");
        }
        // Este método debería ser implementado para obtener el ID del pedido del cliente si es necesario
        private int ObtenerIdPedido(int idCliente)
        {
            // Lógica para obtener el idPedido, puede ser desde la base de datos o alguna lógica propia
            // Para simplificar, retorno un valor fijo (puedes personalizar esto según tu necesidad)
            return 1; // ejemplo de idPedido
        }
    }
}
