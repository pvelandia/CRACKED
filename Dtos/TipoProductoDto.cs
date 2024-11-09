using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class TipoProductoDto
    {
        public int IdTipoProducto { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int IdEstado { get; set; }
    }
}