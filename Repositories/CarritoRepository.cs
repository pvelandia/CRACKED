using CRACKED.Dtos;  // Asegúrate de usar los DTOs
using CRACKED.Models;
using CRACKED.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRACKED.Repositories
{
    public class CarritoRepository
    {
        // Método para obtener un pedido por su ID utilizando DTO
        private readonly CRACKEDEntities36 _context;

        // Constructor que acepta un CRACKEDEntities29 como argumento
        public CarritoRepository(CRACKEDEntities36 context)
        {
            _context = context;
        }
        public bool ActualizarPedido(PedidoDto pedido)
        {
            using (var db = new CRACKEDEntities36()) // Asegúrate de usar tu contexto adecuado
            {
                // Obtener el pedido existente a partir del idPedido
                var pedidoExistente = db.PEDIDOes.SingleOrDefault(p => p.idPedido == pedido.IdPedido);

                if (pedidoExistente != null)
                {
                    // Actualizar los campos del pedido
                    pedidoExistente.barrio = pedido.Barrio;
                    pedidoExistente.direccion = pedido.Direccion;
                    pedidoExistente.fechaEntrega = pedido.FechaEntrega;
                    pedidoExistente.fechaVenta = DateTime.Now;
                    pedidoExistente.telefonoEntrega = pedido.Telefono;
                    //pedidoExistente.idCiudad = pedido.IdCiudad;
                    pedidoExistente.subtotal = pedido.ValorTotal;
                    pedidoExistente.valorDomicilio= 15000;
                    float totali = (float)(pedidoExistente.subtotal + pedidoExistente.valorDomicilio);
                    pedidoExistente.totalPedido = totali ;

                    // Guardar los cambios
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    // Si no se encuentra el pedido, devolver false
                    return false;
                }
            }
        }
        public int? ObtenerIdCiudadPorNombre(string nombreCiudad)
        {
            var ciudad = _context.CIUDADs.FirstOrDefault(c => c.nombre == nombreCiudad);
            return ciudad?.idCiudad;  // Devuelve el Id si encuentra coincidencia, o null si no la hay
        }
        public bool ActualizarDatosEntrega(int idCliente, PedidoDto pedidoDto, string name)
        {
            try
            {
                using (var db = new CRACKEDEntities36())
                {
                    // Obtener el correo del usuario desde la tabla USUARIOS
                    var usuario = db.USUARIOs.FirstOrDefault(u => u.idUsuario == idCliente);
                    string emailDestino = usuario?.correoElectronico; // Asigna el correo si existe

                    var pedido_productos = db.PEDIDO_PRODUCTO.Where(p => p.IdCliente == idCliente).ToList();
                    var pedido = db.PEDIDOes
                                   .Where(p => p.idCliente == idCliente)
                                   .OrderByDescending(p => p.idPedido)
                                   .FirstOrDefault();
                    if (pedido != null && emailDestino != null)
                    {
                        float sub = 0;
                        foreach (var producto in pedido_productos)
                        {
                            sub += (float)producto.valorProducto;
                        }
                        pedido.subtotal = sub;
                        pedido.direccion = pedidoDto.Direccion;
                        pedido.barrio = pedidoDto.Barrio;
                        pedido.telefonoEntrega = pedidoDto.Telefono;
                        pedido.valorDomicilio = 15000;
                        pedido.totalPedido = pedido.valorDomicilio + pedido.subtotal;
                        pedido.idCiudad = pedidoDto.IdCiudads;
                        pedido.fechaVenta = DateTime.Now;
                        pedido.fechaEntrega = pedidoDto.FechaEntrega;

                        db.SaveChanges();

                        var productosPedido = db.PEDIDO_PRODUCTO.Where(pp => pp.IdCliente == idCliente).ToList();
                        string mensajeCorreo = CrearMensajeCorreo(name, pedido, productosPedido);

                        // Enviar el correo
                        Correo gestorCorreo = new Correo();
                        gestorCorreo.EnviarCorreo(emailDestino, "CRACKED - Confirmación de Pedido", mensajeCorreo, true);

                        if (productosPedido.Any())
                        {
                            db.PEDIDO_PRODUCTO.RemoveRange(productosPedido);
                            db.SaveChanges();
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar los datos de entrega", ex);
            }
        }

        private string ObtenerNombreProductoPorId(int idProducto)
        {
            using (var db = new CRACKEDEntities36())
            {
                var producto = db.PRODUCTOes.FirstOrDefault(p => p.idProducto == idProducto);
                return producto != null ? producto.nombre : "Producto no encontrado";
            }
        }

        private string CrearMensajeCorreo(string nombreUsuario, PEDIDO pedido, List<PEDIDO_PRODUCTO> productosPedido)
        {
            string productosHtml = "";

            foreach (var producto in productosPedido)
            {
                // Llama al método para obtener el nombre del producto
                string nombreProducto = ObtenerNombreProductoPorId(producto.Idproducto);

                productosHtml += $@"
            <tr>
                <td style='padding: 10px; border: 1px solid #ddd;'>{producto.idPedidoProducto}</td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{nombreProducto}</td>
                <td style='padding: 10px; border: 1px solid #ddd;'>{producto.cantidad}</td>
                <td style='padding: 10px; border: 1px solid #ddd;'>${producto.valorProducto}</td>
            </tr>";
            }

            return $@"
    <html>
    <body style='font-family: Arial, sans-serif; color: #333;'>
        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
            <h2 style='text-align: center; color: #0056b3;'>Confirmación de tu Pedido - CRACKED</h2>
            <p>Hola <strong>{nombreUsuario}</strong>,</p>
            <p>¡Gracias por tu compra! Aquí tienes los detalles de tu pedido:</p>
            <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                <tr style='background-color: #f8f8f8;'>
                    <th style='padding: 10px; border: 1px solid #ddd;'>IdProducto</th>
<th style='padding: 10px; border: 1px solid #ddd;'>Producto</th>
                    <th style='padding: 10px; border: 1px solid #ddd;'>Cantidad</th>
                    <th style='padding: 10px; border: 1px solid #ddd;'>Precio</th>
                </tr>
                {productosHtml}
            </table>
            <p style='text-align: right; font-size: 18px;'><strong>Total: ${pedido.totalPedido}</strong></p>
            <p>Tu pedido será enviado a:</p>
            <p>
                <strong>Dirección:</strong> {pedido.direccion}<br>
                <strong>Barrio:</strong> {pedido.barrio}<br>
            </p>
            <p>Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos.</p>
            <p style='color: #888;'>Este es un mensaje automático. Por favor, no respondas a este correo.</p>
            <p style='text-align: center; color: #0056b3;'>¡Gracias por confiar en CRACKED!</p>
        </div>
    </body>
    </html>";
        }

        public PedidoDto ObtenerPedidoPorId(int idPedido)
        {
            try
            {
                using (var db = new CRACKEDEntities36())
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
                using (var db = new CRACKEDEntities36())
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
                using (var db = new CRACKEDEntities36())
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
                using (var db = new CRACKEDEntities36())
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
                using (var db = new CRACKEDEntities36())
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
            using (var db = new CRACKEDEntities36())  // Usamos el contexto de la base de datos
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
            // Obtener el producto en el carrito
            var pedidoProducto = _context.PEDIDO_PRODUCTO.FirstOrDefault(p => p.idPedidoProducto == idPedidoProducto);

            if (pedidoProducto != null)
            {
                // Obtener el producto de la tabla PRODUCTO correspondiente
                var producto = _context.PRODUCTOes.FirstOrDefault(p => p.idProducto == pedidoProducto.Idproducto);

                if (producto != null)
                {
                    // Aumentar el stock del producto
                    producto.stock += pedidoProducto.cantidad;  // Se asume que la tabla PEDIDO_PRODUCTO tiene la cantidad

                    // Guardar el cambio en el stock
                    _context.SaveChanges();
                }

                // Eliminar el producto del carrito (de la tabla PEDIDO_PRODUCTO)
                _context.PEDIDO_PRODUCTO.Remove(pedidoProducto);

                // Guardar los cambios (eliminación del producto del carrito)
                _context.SaveChanges();

                return true;
            }

            return false;
        }

    }
}
