using CRACKED.Dtos;  // Asegúrate de incluir los DTOs
using CRACKED.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRACKED.Services
{
    public class CarritoService
    {
        private readonly CarritoRepository _carritoRepository;
        private readonly ProductRepository _productRepository; // Aquí agregamos ProductRepository
    

        public CarritoService(CarritoRepository carritoRepository, ProductRepository productRepository)
        {
            _carritoRepository = carritoRepository;
            _productRepository = productRepository; // Asignamos el productRepository
        }

        // Agregar producto al carrito
        public bool ActualizarPedido(PedidoDto pedido)
        {
            try
            {
                // Llamamos al repositorio para actualizar los datos en la base de datos
                return _carritoRepository.ActualizarPedido(pedido);
            }
            catch (Exception ex)
            {
                // Manejar excepciones si ocurre algún error
                Console.WriteLine("Error al actualizar el pedido: " + ex.Message);
                return false;
            }
        }
        public PedidoDto ObtenerPedidoPorId(int idPedido)
        {
            return _carritoRepository.ObtenerPedidoPorId(idPedido);
        }
        public bool ActualizarDatosEntrega(int idCliente, PedidoDto pedidoDto)
        {
            try
            {
                // Llamamos al método del repositorio
                return _carritoRepository.ActualizarDatosEntrega(idCliente, pedidoDto);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en el servicio
                throw new InvalidOperationException("Error al actualizar los datos de entrega en el servicio", ex);
            }
        }
        public int? ObtenerIdCiudadSiNombreCoincide(string nombreCiudad)
        {
            return _carritoRepository.ObtenerIdCiudadPorNombre(nombreCiudad);
        }

        public void AgregarProductoAlCarrito(int productoId, int cantidad, float precio, int idPedido, int idCliente, int porcion)
        {
            try
            {
                
                // Obtener stock del producto usando el DTO
                ProductDto producto = _productRepository.ObtenerProductoPorId(productoId);
                PedidoDto pedido = _carritoRepository.ObtenerPedidoPorId(idPedido);

                if (producto == null)
                {
                    throw new InvalidOperationException("El producto no existe.");
                }

                int stockDisponible = (int)producto.Stock; // Usamos la propiedad del DTO
              

                // Verificar si la cantidad solicitada supera el stock disponible
                if (cantidad > stockDisponible)
                {
                    throw new InvalidOperationException("La cantidad solicitada supera el stock disponible.");
                }

                // Verificar si el pedido existe usando DTOs
                PedidoDto pedidoExistente = _carritoRepository.ObtenerPedidoPorId(idPedido);

                if (pedidoExistente == null)
                {
                    // Si no existe el pedido, lo creamos usando DTOs
                    var nuevoPedido = _carritoRepository.CrearPedido(idCliente, "Dirección de ejemplo", 45678, 19);
                    idPedido = nuevoPedido.IdPedido;  // Asignamos el id del nuevo pedido
                }

                // Verificar si el producto ya está en el carrito usando DTOs
                Pedido_ProductoDto productoExistente = _carritoRepository.ObtenerProductoEnCarrito(productoId, idCliente, idPedido);

                if (productoExistente == null)
                {
                    // Si el producto no está en el carrito, lo agregamos con DTOs
                    var nuevoProducto = new Pedido_ProductoDto
                    {
                        IdProducto = productoId,
                        IdCliente = idCliente,
                        IdEstado = 1,  // Estado "activo"
                        IdPedido = idPedido,
                        Cantidad = cantidad,
                        Precio = precio,
                        Porcion = porcion
                    };

                    // Agregar el producto al carrito
                    _carritoRepository.AgregarProductoAlCarrito(nuevoProducto);
                }
                else
                {
                    // Si el producto ya existe en el carrito, solo actualizamos la cantidad
                    productoExistente.Cantidad += cantidad;  // Sumar la cantidad
                    productoExistente.Precio += precio;  // Actualizamos el precio

                    // Actualizamos el producto en el carrito con DTOs
                    _carritoRepository.ActualizarCantidadProducto(productoExistente);
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de manera adecuada
                Console.WriteLine("Error al agregar producto al carrito: " + ex.Message);
                throw new InvalidOperationException("Error al agregar el producto al carrito", ex);
            }
        }
        public List<CarroDto> ObtenerCarritoPorCliente(int idCliente)
        {
            // Obtener los productos en el carrito del cliente
            var productosEnCarrito = _carritoRepository.ObtenerCarritoPorCliente(idCliente);

            // Convertir los productos en DTOs para la vista
            var carritoItems = productosEnCarrito.Select(item => new CarroDto
            {
                IdPedidoProducto=item.IdPedidoProducto,
                IdProducto = item.IdProducto,
                Nombre = item.Nombre,  // Asegúrate de que NombreProducto esté disponible en el item
                Cantidad = item.Cantidad,
                PrecioUnit = item.PrecioUnit,
                PrecioTotal = item.PrecioTotal,  // Este debería ser el precio total calculado

            }).ToList();
            float precioSubtotal = carritoItems.Sum(item => item.PrecioTotal);

            // Asignar el subtotal a cada item si lo deseas
            foreach (var item in carritoItems)
            {
                item.PrecioSubtotal = precioSubtotal; // Asignar el mismo subtotal a cada producto (si deseas mostrarlo por item)
            }


            // Retornar tanto los items del carrito como el subtotal
            return carritoItems;

        }
        public bool EliminarProducto(int idPedidoProducto)
        {
            return _carritoRepository.EliminarProductoDelCarrito(idPedidoProducto);
        }
    }
}
