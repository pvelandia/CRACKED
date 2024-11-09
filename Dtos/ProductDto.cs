using System;

namespace CRACKED.Dtos
{
    public class ProductDto
    {
        public int IdProducto { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public float? Precio { get; set; }

        // Cambiar a int? para permitir que sean null
        public int? Stock { get; set; }
        public int? Porcion { get; set; }

        public int IdSabor { get; set; }
        public string Imagen { get; set; } // URL de la imagen del producto

        public int IdTipoProducto { get; set; } // Enlace con el tipo de producto
    }
}
