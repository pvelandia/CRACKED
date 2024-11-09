
using CRACKED.Models;
using System;
using System.Linq;

namespace CRACKED.Repositories
{
    public class CarritoRepository
    {
        private readonly CRACKEDEntities25 _context;

        // Constructor para inyectar el contexto de la base de datos
        public CarritoRepository(CRACKEDEntities25 context)
        {
            _context = context;
        }

        // Verificar si el pedido ya existe
        public PEDIDO ObtenerPedidoPorId(int idPedido)
        {
            return _context.PEDIDOes.FirstOrDefault(p => p.idPedido == idPedido);
        }

        // Crear un nuevo pedido
        public PEDIDO CrearPedido(int idCliente, string direccion, float valor, float iva)
        {
            var pedidoExistente = _context.PEDIDOes
       .FirstOrDefault(p => p.idCliente == idCliente && p.direccion == direccion);

            if (pedidoExistente != null)
            {
                // Si el pedido ya existe, actualizamos los valores necesarios
                pedidoExistente.fechaEntrega = DateTime.Now;
                pedidoExistente.fechaVenta = DateTime.Now;
                pedidoExistente.direccion = direccion;
                //pedidoExistente.valor = valor;
                //pedidoExistente.iva = iva;
                pedidoExistente.idDomiciliario = 4;
                pedidoExistente.idCiudad = 1;
                pedidoExistente.idMetodoPago = 1;
                pedidoExistente.idEstado = 1;

                _context.SaveChanges(); // Guardar los cambios
                return pedidoExistente; // Retornar el pedido actualizado
            }
            else
            {
                var nuevoPedido = new PEDIDO
                {
                    //idPedido = ,
                    idCliente = idCliente,
                    idDomiciliario = 4,  // Asumimos que esto se establece de alguna manera
                    idCiudad = 1,        // Lo mismo aquí
                    idMetodoPago = 1,    // Lo mismo
                    idEstado = 1,        // Lo mismo
                    fechaEntrega = DateTime.Now,
                    fechaVenta = DateTime.Now,
                    direccion = direccion,
                    //valor = valor,
                    //iva = iva
                };

                _context.PEDIDOes.Add(nuevoPedido);
                _context.SaveChanges(); // Esto genera el idPedido automáticamente

                return nuevoPedido;  // Retornamos el pedido con su id generado
            }
        }

        // Verificar si el producto ya está en el carrito
        public PEDIDO_PRODUCTO ObtenerProductoEnCarrito(int productoId, int idCliente, int idPedido)
        {
            return _context.PEDIDO_PRODUCTO
                .FirstOrDefault(p => p.Idproducto == productoId && p.IdCliente == idCliente && p.idPedido == idPedido);
        }

        // Agregar un producto al carrito
        public void AgregarProductoAlCarrito(PEDIDO_PRODUCTO producto)
        {
            _context.PEDIDO_PRODUCTO.Add(producto);
            _context.SaveChanges();
        }

        // Actualizar la cantidad de un producto en el carrito
        public void ActualizarCantidadProducto(PEDIDO_PRODUCTO producto)
        {
            _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
