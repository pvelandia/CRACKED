using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRACKED.Models;
using CRACKED.Dtos;
using CRACKED.Repositories;

namespace CRACKED.Services
{
    public class RegistroProductoService 
    {
        private readonly RegistroProductoRepository _registroProductoRepository;

        public RegistroProductoService(RegistroProductoRepository registroProductoRepository)
        {
            _registroProductoRepository = registroProductoRepository;
        }

        public List<ProductosPedidoRDTO> ObtenerProductosDePedido(int id)
        {
            return _registroProductoRepository.ObtenerProductosPorPedido(id);
        }
    }
}