using CRACKED.Dtos;  // Asegúrate de usar los DTOs
using CRACKED.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRACKED.Repositories
{
    public class CarritoRepository
    {
        // Método para obtener un pedido por su ID utilizando DTO
        private readonly CRACKEDEntities31 _context;

        // Constructor que acepta un CRACKEDEntities29 como argumento
        public CarritoRepository(CRACKEDEntities31 context)
        {
            _context = context;
        }

        public PedidoDto ObtenerPedidoPorId(int idPedido)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    // Buscamos el pedido en la base de datos y lo mapeamos a un DTO
                    var pedido = db.PEDIDOes
                        .Where(p => p.idPedido == idPedido)
                        .Select(p => new PedidoDto
                        {
                            IdPedido = p.idPedido,
                            IdCliente = p.idCliente,
                            Direccion = p.direccion,
                            FechaVenta = (DateTime)p.fechaVenta,
                            FechaEntrega = (DateTime)p.fechaEntrega,
                            IdEstado = p.idEstado
                        })
                        .FirstOrDefault();

                    return pedido;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el pedido: " + ex.Message);
                return null;
            }
        }

        // Método para crear o actualizar un pedido utilizando DTO
        public PedidoDto CrearPedido(int idCliente, string direccion, float valor, float iva)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    // Buscamos si ya existe un pedido para el cliente con la misma dirección
                    var pedidoExistente = db.PEDIDOes
                        .FirstOrDefault(p => p.idCliente == idCliente && p.direccion == direccion);

                    if (pedidoExistente != null)
                    {
                        // Si el pedido existe, actualizamos los valores necesarios
                        pedidoExistente.fechaEntrega = DateTime.Now;
                        pedidoExistente.fechaVenta = DateTime.Now;
                        pedidoExistente.direccion = direccion;
                        pedidoExistente.idDomiciliario = 4; // Asumimos que esto se establece de alguna manera
                        pedidoExistente.idCiudad = 1; // Lo mismo aquí
                        pedidoExistente.idMetodoPago = 1; // Lo mismo
                        pedidoExistente.idEstado = 1; // Asumimos que el estado es 1

                        db.SaveChanges(); // Guardar los cambios

                        // Retornar el pedido actualizado como DTO
                        return new PedidoDto
                        {
                            IdPedido = pedidoExistente.idPedido,
                            IdCliente = pedidoExistente.idCliente,
                            Direccion = pedidoExistente.direccion,
                            FechaVenta = (DateTime)pedidoExistente.fechaVenta,
                            FechaEntrega = (DateTime)pedidoExistente.fechaEntrega,
                            IdEstado = pedidoExistente.idEstado
                        };
                    }
                    else
                    {
                        // Si el pedido no existe, creamos uno nuevo
                        var nuevoPedido = new PEDIDO
                        {
                            idCliente = idCliente,
                            idDomiciliario = 4,  // Asumimos que esto se establece de alguna manera
                            idCiudad = 1,        // Lo mismo aquí
                            idMetodoPago = 1,    // Lo mismo
                            idEstado = 1,        // Lo mismo
                            fechaEntrega = DateTime.Now,
                            fechaVenta = DateTime.Now,
                            direccion = direccion,
                        };

                        db.PEDIDOes.Add(nuevoPedido);
                        db.SaveChanges(); // Esto genera el idPedido automáticamente

                        // Retornar el nuevo pedido como DTO
                        return new PedidoDto
                        {
                            IdPedido = nuevoPedido.idPedido,
                            IdCliente = nuevoPedido.idCliente,
                            Direccion = nuevoPedido.direccion,
                            FechaVenta = (DateTime)nuevoPedido.fechaVenta,
                            FechaEntrega = (DateTime)nuevoPedido.fechaEntrega,
                            IdEstado = nuevoPedido.idEstado
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear o actualizar el pedido: " + ex.Message);
                return null;
            }
        }

        // Método para obtener un producto en el carrito utilizando DTO
        public Pedido_ProductoDto ObtenerProductoEnCarrito(int productoId, int idCliente, int idPedido)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    // Buscamos el producto en el carrito y lo mapeamos a un DTO
                    var producto = db.PEDIDO_PRODUCTO
                        .Where(p => p.Idproducto == productoId && p.IdCliente == idCliente && p.idPedido == idPedido)
                        .Select(p => new Pedido_ProductoDto
                        {
                            IdProducto = p.Idproducto,
                            IdCliente = p.IdCliente,
                            IdPedido = p.idPedido,
                            Cantidad = p.cantidad,
                            Precio = (double)p.valorProducto,
                            IdEstado = p.IdEstado

                        })
                        .FirstOrDefault();

                    return producto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el producto en el carrito: " + ex.Message);
                return null;
            }
        }

        // Método para agregar un producto al carrito utilizando DTO
        public void AgregarProductoAlCarrito(Pedido_ProductoDto producto)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    var nuevoProducto = new PEDIDO_PRODUCTO
                    {
                        Idproducto = producto.IdProducto,
                        IdCliente = producto.IdCliente,
                        idPedido = producto.IdPedido,
                        cantidad = producto.Cantidad,
                        valorProducto = producto.Precio, // Convertir el precio a decimal si es necesario
                        IdEstado = producto.IdEstado,
                        porcion = producto.Porcion
                    };

                    db.PEDIDO_PRODUCTO.Add(nuevoProducto);
                    db.SaveChanges(); // Guardar el nuevo producto en el carrito
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar el producto al carrito: " + ex.Message);
            }
        }

        // Método para actualizar la cantidad de un producto en el carrito utilizando DTO
        public void ActualizarCantidadProducto(Pedido_ProductoDto producto)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    var productoExistente = db.PEDIDO_PRODUCTO
                        .FirstOrDefault(p => p.Idproducto == producto.IdProducto && p.IdCliente == producto.IdCliente && p.idPedido == producto.IdPedido);

                    if (productoExistente != null)
                    {
                        // Actualizamos la cantidad y el precio del producto
                        productoExistente.cantidad = producto.Cantidad;
                        productoExistente.valorProducto = producto.Precio; // Convertir el precio si es necesario
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la cantidad del producto: " + ex.Message);
            }
        }
        public List<CarroDto> ObtenerCarritoPorCliente(int idCliente)
        {
            using (var db = new CRACKEDEntities31())  // Usamos el contexto de la base de datos
            {
                // Realizamos la consulta para obtener los productos del carrito
                var carritoItems = db.PEDIDO_PRODUCTO
                    .Where(c => c.IdCliente == idCliente)  // Filtramos por el ID del cliente
                    .Join(
                        db.PRODUCTOes,  // Hacemos el join con la tabla Producto
                        pedidoProducto => pedidoProducto.Idproducto,  // Relación con el IdProducto de PEDIDO_PRODUCTO
                        producto => producto.idProducto,  // Relación con el IdProducto de Producto
                        (pedidoProducto, producto) => new CarroDto
                        {
                            IdPedidoProducto = pedidoProducto.idPedidoProducto,
                            IdProducto = pedidoProducto.Idproducto,  // El ID del producto
                    Nombre = producto.nombre,  // Nombre del producto de la tabla Producto
                    PrecioUnit = (float)producto.valorUnitario,  // El valor del producto
                    Cantidad = (int)pedidoProducto.cantidad,  // La cantidad seleccionada
                    PrecioTotal = (float)pedidoProducto.valorProducto,  // El total calculado
                      
                        })
                    .ToList();
                float PrecioSubtotal = 0;
                foreach (var item in carritoItems)
                {
                    PrecioSubtotal += (float)item.PrecioTotal;  // Sumar el total de cada producto
                }
                return carritoItems;  // Retornamos la lista de productos en el carrito
            }
        }

        public bool EliminarProductoDelCarrito(int idPedidoProducto)
        {
            var producto = _context.PEDIDO_PRODUCTO.FirstOrDefault(p => p.idPedidoProducto== idPedidoProducto);
            if (producto != null)
            {
                _context.PEDIDO_PRODUCTO.Remove(producto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
