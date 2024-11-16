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
        private readonly CRACKEDEntities41 _context;

        // Constructor que recibe el contexto
        public PedidoRepository(CRACKEDEntities41 context)
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
                    Ciudad = p.CIUDAD.nombre,
                    Telefono = p.telefonoEntrega,
                    Correo = p.USUARIO.correoElectronico,
                    ValorPedido = p.totalPedido,
                    Domiciliario=p.USUARIO.nombre,
                    IdEstado = p.ESTADO.idEstado,
                    ApellidoCliente = p.USUARIO.apellido

                }).ToList();  // Cambiado a ToList()
        }
        public PedidoAdminDTO ObtenerPedidoPorId(int id)
        {
            return _context.PEDIDOes
                .Where(p => p.idPedido == id)
                .Select(p => new PedidoAdminDTO
                {
                    IdPedido = p.idPedido,
                    Direccion = p.direccion,
                    Estado = p.ESTADO.nombre,
                    ClienteNombre = p.USUARIO.nombre,
                    FechaEntrega = p.fechaEntrega,
                    Ciudad=p.CIUDAD.nombre,
                    Telefono=p.telefonoEntrega,
                    Correo=p.USUARIO.correoElectronico,
                    ValorPedido=p.totalPedido,
                    Domiciliario = p.USUARIO.nombre,
                    IdEstado = p.ESTADO.idEstado,
                    ApellidoCliente=p.USUARIO.apellido

                }).FirstOrDefault();
        }
        public void ActualizarEstadoPedido(int idPedido, int idEstado)
        {
            var pedido = _context.PEDIDOes.FirstOrDefault(p => p.idPedido == idPedido);
            if (pedido != null)
            {
                pedido.idEstado = idEstado;  // Actualizar el estado del pedido
                _context.SaveChanges();      // Guardar los cambios en la base de datos
            }
        }
    }
}
