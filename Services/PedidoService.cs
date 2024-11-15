using CRACKED.Dtos;
using CRACKED.Repositories;
using System.Collections.Generic;
using System.Web;
using System;
using System.Threading.Tasks;

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

    }
}
