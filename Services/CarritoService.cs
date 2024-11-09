using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRACKED.Services
{
    public class CarritoService
    {
        private readonly CarritoRepository _carritoRepository;

        public CarritoService()
        {
            _carritoRepository = new CarritoRepository(); // Inicializamos el repositorio
        }

        // Agrega un producto al carrito
        public void AgregarProductoAlCarrito(int productoId, int cantidad, float precio, int idPedido, int idCliente)
        {
            // Verificamos si el producto ya está en el carrito
            var productoExistente = _carritoRepository.ObtenerProductoEnCarrito(productoId, idCliente, idPedido);

            if (productoExistente == null)
            {
                // Si el producto no está en el carrito, lo agregamos
                var nuevoProducto = new PEDIDO_PRODUCTO
                {
                    Idproducto = productoId,
                    IdCliente = idCliente,
                    IdEstado = 1,
                    idPedido = idPedido,
                    cantidad = cantidad,
                    valorProducto = precio
                };

                _carritoRepository.AgregarProductoAlCarrito(nuevoProducto);
            }
            else
            {
                // Si el producto ya existe en el carrito, actualizamos la cantidad
                productoExistente.cantidad += cantidad; // Se suma la cantidad
                productoExistente.valorProducto = precio; // Se actualiza el precio si es necesario

                _carritoRepository.ActualizarCantidadProducto(productoExistente);
            }
        }

        // Elimina un producto del carrito
        public void EliminarProductoDelCarrito(int productoId, int idCliente, int idPedido)
        {
            _carritoRepository.EliminarProductoDelCarrito(productoId, idCliente, idPedido);
        }

        // Obtiene los productos del carrito de un cliente
        public List<CarritoProductoDto> ObtenerProductosEnCarrito(int idCliente, int idPedido)
        {
            var productos = _carritoRepository.ObtenerProductosEnCarrito(idCliente, idPedido)
                .Select(p => new CarritoProductoDto
                {
                    IdProducto = p.Idproducto,
                    Cantidad = p.cantidad,
                    Precio = (float)p.valorProducto
                }).ToList();

            return productos;
        }
    }
}
