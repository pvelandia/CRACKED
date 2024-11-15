using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRACKED.Dtos;
using CRACKED.Models;
using System.Data.Entity;

namespace CRACKED.Repositories
{
   

    public class PedidoRepository 
    {
        private readonly CRACKEDEntities37 _context;

        // Constructor que recibe el contexto
        public PedidoRepository(CRACKEDEntities37 context)
        {
            _context = context;
        }

        // Método asincrónico para obtener los pedidos con relaciones cargadas
        public List<PedidoAdminDTO> ObtenerPedidosAdmin()
        {
            return _context.PEDIDOes
                .Select(p => new PedidoAdminDTO
                {
                    IdPedido = p.idPedido,
                    Direccion = p.direccion,
                    Estado = p.ESTADO.nombre,
                    ClienteNombre = p.USUARIO.nombre,  // Asumimos que el Cliente tiene un nombre
                FechaEntrega = p.fechaEntrega,
                }).ToList();  // Cambiado a ToList()
        }

    }
}
