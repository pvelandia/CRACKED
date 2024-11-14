using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class ReportDto2
    {
        public List<ReportDto> List { get; set; }
        public string GeneradoPor { get; set; }
        public string Fecha { get; set; }
        public string TipoReporte { get; set; }
    }
}