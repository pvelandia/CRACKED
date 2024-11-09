using CRACKED.Models;
using CRACKED.Repositories;
using System;

namespace CRACKED.Services
{
    public class CarritoService
    {
        private readonly CarritoRepository _carritoRepository;

        // Constructor para inyectar el repositorio
        public CarritoService(CarritoRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        // Agregar producto al carrito
        public void AgregarProductoAlCarrito(int productoId, int cantidad, float precio, int idPedido, int idCliente)
        {
            try
            {
                // Verificar si el pedido existe
                var pedidoExistente = _carritoRepository.ObtenerPedidoPorId(idPedido);

                if (pedidoExistente == null)
                {
                    // Si no existe el pedido, lo creamos
                    var nuevoPedido = _carritoRepository.CrearPedido(idCliente, "Dirección de ejemplo", 45678, 19);
                    idPedido = nuevoPedido.idPedido;  // Asignamos el id del nuevo pedido
                }

                // Verificar si el producto ya está en el carrito
                var productoExistente = _carritoRepository.ObtenerProductoEnCarrito(productoId, idCliente, idPedido);

                if (productoExistente == null)
                {
                    // Si el producto no está en el carrito, lo agregamos
                    var nuevoProducto = new PEDIDO_PRODUCTO
                    {
                        Idproducto = productoId,
                        IdCliente = idCliente,
                        IdEstado = 1,  // Estado "activo"
                        idPedido = idPedido,
                        cantidad = cantidad,
                        valorProducto = precio
                    };

                    // Agregar el producto al carrito
                    _carritoRepository.AgregarProductoAlCarrito(nuevoProducto);
                }
                else
                {
                    // Si el producto ya existe en el carrito, solo actualizamos la cantidad
                    productoExistente.cantidad += cantidad;  // Sumar la cantidad
                    productoExistente.valorProducto = precio;  // Actualizamos el precio

                    // Actualizamos el producto en el carrito
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
    }
}
