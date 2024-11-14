using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Dtos
{
    public class PedidoListDto
    {
        public List<PedidoDto> Pedidos { get; set; } = new List<PedidoDto>();

    }
}