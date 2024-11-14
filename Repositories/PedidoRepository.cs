using System;
using System.Collections.Generic;
using System.Linq;
using CRACKED.Dtos;
using CRACKED.Models;

namespace CRACKED.Repositories
{
    public class PedidoRepository
    {
        public PedidoListDto ObtenerPedidos()
        {
            var pedidoListDto = new PedidoListDto();

            try
            {
                using (var db = new CRACKEDEntities35())
                {
                    // Realizamos los joins para traer la información relacionada
                    var pedidos = (from p in db.PEDIDOes
                                   join c in db.USUARIOs on p.idCliente equals c.idUsuario into clienteJoin
                                   from c in clienteJoin.DefaultIfEmpty()  // Left join para Cliente
                                   join d in db.USUARIOs on p.idDomiciliario equals d.idUsuario into domiciliarioJoin
                                   from d in domiciliarioJoin.DefaultIfEmpty()  // Left join para Domiciliario
                                   join e in db.ESTADOes on p.idEstado equals e.idEstado
                                   join ci in db.CIUDADs on p.idCiudad equals ci.idCiudad
                                  
                                   select new PedidoDto
                                   {
                                       IdPedido = p.idPedido,
                                       IdCliente = p.idCliente,
                                       IdDomiciliario = p.idDomiciliario,
                                       IdCiudads = p.idCiudad, 
                                       IdEstado = p.idEstado,
                                       FechaEntrega = p.fechaEntrega.HasValue ? p.fechaEntrega.Value : default(DateTime),
                                       FechaVenta = p.fechaVenta.HasValue ? p.fechaVenta.Value : default(DateTime),
                                       Direccion = p.direccion,
                                       Barrio = p.barrio,
                                       Telefono = p.telefonoEntrega,
                                       ValorTotal = (float)p.totalPedido,
                                       NombreCliente = c != null ? c.nombre : "Desconocido",  // Si Cliente es null
                                       NombreDomiciliario = d != null ? d.nombre : "Desconocido",  // Lo mismo para Domiciliario
                                       NombreEstado = e.nombre,
                                       NombreCiudad = ci.nombre,
                                      
                                   }).ToList();

                    // Asignamos los pedidos a la propiedad Pedidos de PedidoListDto
                    pedidoListDto.Pedidos = pedidos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener pedidos: " + ex.Message);
            }

            return pedidoListDto;
        }
    }
}
