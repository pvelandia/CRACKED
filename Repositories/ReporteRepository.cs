using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using CRACKED.Dtos;
    using CRACKED.Models;

    public class ReporteRepository
    {
        public List<ReportDto> ObtenerPedidos()
        {
            using (var db = new CRACKEDEntities39())
            {
                return db.PEDIDOes.Select(p => new ReportDto
                {
                    IdPedido = p.idPedido,
                    IdCliente = p.idCliente,
                    IdEstado = p.idEstado,
                    FechaEntrega = (DateTime)p.fechaEntrega,
                    FechaVenta = (DateTime)p.fechaVenta,
                    Direccion = p.direccion,
                    ValorTotal = (float)p.totalPedido
                }).ToList();
            }
        }

        public List<ReportDto> ObtenerUsuarios()
        {
            using (var db = new CRACKEDEntities39())
            {
                return db.USUARIOs.Select(u => new ReportDto
                {
                    IdUser = u.idUsuario,
                    IdRol = u.idRol,
                    Telefono = u.numeroContacto,
                    IdEstado = u.idEstado,
                    Name = u.nombre,
                    Apellido = u.apellido,
                    Correo = u.correoElectronico
                }).ToList();
            }
        }
    }

}