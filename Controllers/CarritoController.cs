using System;
using System.Linq;
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
        private readonly ProductRepository _productRepository;

        // Constructor sin parámetros
        public CarritoController()
        {
            // Aquí se pasa tanto CarritoRepository como ProductRepository al CarritoService
            _carritoService = new CarritoService(
                new CarritoRepository(new CRACKEDEntities40()),
                new ProductRepository()
            );
        }

        // Constructor con parámetros (necesario cuando tienes IoC configurado)
        public CarritoController(CarritoService carritoService, ProductRepository productRepository)
        {
            _carritoService = carritoService;
            _productRepository = productRepository;
        }


        [HttpPost]
        public ActionResult DatosEntrega(PedidoDto model)
        {
            try
            {
                // Validación de los datos recibidos
                if (ModelState.IsValid)
                {
                    // Verificamos si el usuario está autenticado
                    var user = Session["UserLogged"] as UserDto;
                    if (user == null)
                    {
                        return RedirectToAction("InicioSesion", "User");
                    }
                    // Llama al servicio para obtener el Id de la ciudad basado en el nombre
                    int? idCiudad = _carritoService.ObtenerIdCiudadSiNombreCoincide(model.IdCiudad);

                    if (idCiudad.HasValue)
                    {
                        // Aquí puedes trabajar con el idCiudad encontrado
                        model.IdCiudads = idCiudad.Value;
                    }
                    else
                    {
                        // Maneja el caso en que la ciudad no se encuentra
                        ViewBag.ErrorMessage = "La ciudad especificada no se encuentra.";
                    }
                    int idCliente = user.IdUser;
                  
                    string name = user.Name;
                    // Actualizamos los datos de entrega en el servicio
                    bool actualizado = _carritoService.ActualizarDatosEntrega(idCliente, model, name);
                    
                        return RedirectToAction("Productos", "User");
                    
                    
                }

                // Si el modelo no es válido, volvemos a cargar la vista con los datos ingresados
                return View(model);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, en caso de errores inesperados
                ViewBag.ErrorMessage = "Ocurrió un error: " + ex.Message;
                return View(model);  // Asegúrate de pasar el objeto 'model' de vuelta a la vista
            }
        }

        public ActionResult DatosEntrega()
        {
            //    // Obtenemos el ID del cliente desde la sesión
            //    var user = Session["UserLogged"] as UserDto;
            //    if (user == null)
            //    {
            //        return RedirectToAction("InicioSesion", "User");
            //    }

            //    int idCliente = user.IdUser;


            //    //// Llamamos al servicio para obtener los datos del pedido
            //    //var pedido = _carritoService.ObtenerPedidoPorId(idCliente);

            //    //if (pedido == null)
            //    //{
            //    //    // Si no hay un pedido, podemos crear uno nuevo o redirigir al usuario
            //    //    return RedirectToAction("Carrito");
            //    //}

            //    // Aquí cargamos la información del pedido en el modelo de la vista
            //    var modelo = new PedidoDto
            //    {
            //        IdPedido = pedido.IdPedido,
            //        Direccion = pedido.Direccion,
            //        Barrio = pedido.Barrio,  // Suponiendo que ya tienes el campo Barrio en la base de datos
            //        FechaEntrega = DateTime.Now,  // Si quieres la fecha actual
            //        Telefono = pedido.Telefono,

            //        IdCiudad = pedido.IdCiudad
            //    };





            return View();
        }


        //public ActionResult ActualizarPedido(int idPedido, string barrio, string direccion, DateTime fechaEntrega, string telefono, int idDepartamento, int idCiudad)
        //{

        //    // Obtener el pedido existente a partir del idPedido
        //    int idPedidos = (int?)Session["IdPedido"] ?? 0;
        //    var pedido = _carritoService.ObtenerPedidoPorId(idPedidos);

        //    if (pedido != null)
        //    {
        //        // Actualizar los campos del pedido con los valores recibidos del formulario
        //        pedido.Barrio = barrio;
        //        pedido.Direccion = direccion;
        //        pedido.FechaEntrega = fechaEntrega;
        //        pedido.Telefono = telefono;

        //        pedido.IdCiudad = idCiudad;

        //        // Actualizar el pedido en la base de datos
        //        bool resultado = _carritoService.ActualizarPedido(pedido);

        //        if (resultado)
        //        {
        //            // Redirigir a una vista de confirmación o a la vista de detalles del pedido
        //            return RedirectToAction("ConfirmacionPedido", new { idPedido = pedido.IdPedido });
        //        }
        //        else
        //        {
        //            // Si la actualización falla, mostrar un mensaje de error
        //            ViewBag.ErrorMessage = "Hubo un error al actualizar el pedido.";
        //            return View(pedido); // Puedes redirigir a la misma vista si lo deseas
        //        }
        //    }

        //    // Si el pedido no existe, redirigir a una página de error
        //    return RedirectToAction("Error");
        //}




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
                    Session["ProductoId"] = productoId;
                    float precioTotal = precio * cantidad;
                    precio = precioTotal;

                    int idCliente = user.IdUser;
                    int idPedido = ObtenerIdPedido(idCliente); // Método que debe retornar el idPedido asociado
                    if (porcion == 8)
                    {
                        precio = precio * 2;
                    }
                    //bool stockReducido = _productRepository.ReducirStockProducto(productoId, cantidad);

                    //if (!stockReducido)
                    //{
                    //    throw new InvalidOperationException("El stock es insuficiente para este producto.");
                    //}
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
                IdCiudad = "",
 
                IdEstado = 1,
                FechaEntrega = DateTime.Now,
                FechaVenta = DateTime.Now,
                Direccion = "cra 6 #14-53",
                ValorTotal = 4567,

          
            };

            // Supongamos que no se han pasado los productos
            int productoId = 0;
            int cantidad = 0;
            float precio = 0;
            int idPedido = 0;
            int porcion = 0;

            // Agregar el producto al carrito
            _carritoService.AgregarProductoAlCarrito(productoId, cantidad, precio, idPedido, idCliente, porcion);
            int idPedidos = ObtenerIdPedido(idCliente);  // Método que retorna el idPedido asociado al cliente
            Session["IdPedido"] = idPedidos;  // Guardar el idPedido en la sesión

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
