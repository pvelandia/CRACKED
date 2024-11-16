using CRACKED.Dtos;
using CRACKED.Repositories;
using System.Collections.Generic;
using System.Web;
using System;
using System.Threading.Tasks;
using CRACKED.Models;

namespace CRACKED.Services
{
   
    public class PedidoService 
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public List<PedidoAdminDTO> ObtenerPedidos()
        {
            return _pedidoRepository.ObtenerPedidosAdmin();
        }
        public PedidoAdminDTO ObtenerPedidoPorId(int id)
        {
          return _pedidoRepository.ObtenerPedidoPorId(id);
           
        }
        public void ActualizarEstadoPedido(int idPedido, int idEstado)
        {
            _pedidoRepository.ActualizarEstadoPedido(idPedido, idEstado);
        }
    }
}
