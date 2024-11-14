using CRACKED.Dtos;
using CRACKED.Repositories;
using System.Collections.Generic;
using System.Web;
using System;

namespace CRACKED.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService()
        {
            _pedidoRepository = new PedidoRepository();
        }

        // Método para obtener todos los pedidos
        public PedidoListDto ObtenerPedidos()
        {
            try
            {
                // Aquí podemos agregar cualquier lógica adicional si es necesario
                return _pedidoRepository.ObtenerPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio: " + ex.Message);
                return new PedidoListDto(); // Retornamos un DTO vacío en caso de error
            }
        }
    }
}
