using CRACKED.Models;
using System;
using System.Linq;

namespace CRACKED.Repositories
{
    public class CarritoRepository
    {
        // Verifica si un producto ya existe en el carrito del cliente
        public PEDIDO_PRODUCTO ObtenerProductoEnCarrito(int productoId, int idCliente, int idPedido)
        {
            using (var db = new CRACKEDEntities27())
            {
                return db.PEDIDO_PRODUCTO
                    .FirstOrDefault(cp => cp.Idproducto == productoId && cp.IdCliente == idCliente && cp.idPedido == idPedido);
            }
        }

        // Agrega un nuevo producto al carrito
        public void AgregarProductoAlCarrito(PEDIDO_PRODUCTO pedidoProducto)
        {
            using (var db = new CRACKEDEntities27())
            {
                db.PEDIDO_PRODUCTO.Add(pedidoProducto);
                db.SaveChanges();
            }
        }

        // Actualiza la cantidad de un producto en el carrito
        public void ActualizarCantidadProducto(PEDIDO_PRODUCTO carritoProducto)
        {
            using (var db = new CRACKEDEntities27())
            {
                var productoExistente = db.PEDIDO_PRODUCTO
                    .FirstOrDefault(cp => cp.Idproducto == carritoProducto.Idproducto && cp.IdCliente == carritoProducto.IdCliente && cp.idPedido == carritoProducto.idPedido);

                if (productoExistente != null)
                {
                    productoExistente.cantidad = carritoProducto.cantidad;
                    productoExistente.valorProducto = carritoProducto.valorProducto; // Si es necesario actualizar el precio
                    db.SaveChanges();
                }
            }
        }

        // Elimina un producto del carrito
        public void EliminarProductoDelCarrito(int productoId, int idCliente, int idPedido)
        {
            using (var db = new CRACKEDEntities27())
            {
                var productoExistente = db.PEDIDO_PRODUCTO
                    .FirstOrDefault(cp => cp.Idproducto == productoId && cp.IdCliente == idCliente && cp.idPedido == idPedido);

                if (productoExistente != null)
                {
                    db.PEDIDO_PRODUCTO.Remove(productoExistente);
                    db.SaveChanges();
                }
            }
        }

        // Obtiene los productos de un carrito
        public IQueryable<PEDIDO_PRODUCTO> ObtenerProductosEnCarrito(int idCliente, int idPedido)
        {
            using (var db = new CRACKEDEntities27())
            {
                return db.PEDIDO_PRODUCTO
                    .Where(cp => cp.IdCliente == idCliente && cp.idPedido == idPedido);
            }
        }
    }
}
