using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Services
{
    using System.Collections.Generic;
    using CRACKED.Dtos;
    using CRACKED.Repositories;

    public class ReporteService
    {
        private readonly ReporteRepository _reporteRepository;

        public ReporteService()
        {
            _reporteRepository = new ReporteRepository();
        }

        public List<ReportDto> ObtenerReportePedidos()
        {
            return _reporteRepository.ObtenerPedidos();
        }

        public List<ReportDto> ObtenerReporteUsuarios()
        {
            return _reporteRepository.ObtenerUsuarios();
        }
    }

}