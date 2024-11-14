using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRACKED.Dtos;
using CRACKED.Models;


namespace CRACKED.Repositories
{
    public class PedidoRepository
    {
        //    public PedidoListDto ObtenerPedidosAdmin()
        //    {
        //        PedidoListDto pedidoListDto = new PedidoListDto();

        //        try
        //        {
        //            using (var db = new CRACKEDEntities35())
        //            {
        //                pedidoListDto.Pedidos = db.PEDIDOes
        //                    .Select(p => new PedidoDto
        //                    {
        //                        IdPedido = p.idPedido,
        //                        IdCliente = p.idCliente,
        //                        IdDomiciliario = p.idDomiciliario,
        //                        IdCiudad = p.idCiudad,
        //                        IdCiudads = p.idCiudads,
        //                        IdDepartamento = p.idDepartamento,
        //                        IdEstado = p.idEstado,
        //                        FechaEntrega = p.fechaEntrega,
        //                        FechaVenta = p.fechaVenta,
        //                        Direccion = p.direccion,
        //                        Barrio = p.barrio,
        //                        Telefono = p.telefono,
        //                        ValorTotal = (float)p.valorTotal,

        //                        // Mapea los nombres de las entidades relacionadas
        //                        NombreCliente = p.CLIENTE != null ? p.CLIENTE.nombre : "",  // Mapea nombre del cliente
        //                        NombreDomiciliario = p.DOMICILIARIO != null ? p.DOMICILIARIO.nombre : "",  // Mapea nombre del domiciliario
        //                        NombreEstado = p.ESTADO != null ? p.ESTADO.nombre : "",  // Mapea nombre del estado del pedido
        //                        NombreCiudad = p.CIUDAD != null ? p.CIUDAD.nombre : "",  // Mapea nombre de la ciudad
        //                        NombreDepartamento = p.DEPARTAMENTO != null ? p.DEPARTAMENTO.nombre : ""  // Mapea nombre del departamento
        //                    })
        //                    .ToList();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Error al obtener pedidos: " + ex.Message);
        //        }

        //        return pedidoListDto;
        //    }

        //}
    }
}