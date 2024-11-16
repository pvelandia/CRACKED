using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRACKED.Models;
using CRACKED.Dtos;

namespace CRACKED.Repositories
{
    public class RegistroProductoRepository : Controller
    {
        private readonly CRACKEDEntities39 _context;

        public RegistroProductoRepository(CRACKEDEntities39 context)
        {
            _context = context;
        }

        public List<ProductosPedidoRDTO> ObtenerProductosPorPedido(int idPedido)
        {
            var productos = (from rp in _context.REGISTRO_PRODUCTOS
                             join p in _context.PRODUCTOes on rp.idProducto equals p.idProducto

                             where rp.idPedido == idPedido
                             select new ProductosPedidoRDTO
                             {
                IdPedidoProducto = rp.idPedidoProducto,  // El valor puede ser null dependiendo de la columna
                IdPedido = rp.idPedido,
                IdCliente = rp.idCliente,
                IdProducto = rp.idProducto,
                Cantidad = rp.cantidad,
                IdEstado = rp.idEstado,
                ValorProducto = (double?)rp.valorProducto,
                Porcion = rp.porcion,
                FechaEliminacion = rp.fecha_eliminacionTabla,
                NombreProducto=p.nombre,
            })
            .ToList();

        return productos;
        }
    }
}