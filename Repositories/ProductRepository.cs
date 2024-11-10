using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Linq;

namespace CRACKED.Repositories
{
    public class ProductRepository
    {
        // Método para obtener el stock de un producto específico utilizando DTO
        public int ObtenerStockProducto(int productoId)
        {
            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    // Buscamos el producto en la base de datos y obtenemos su stock
                    var producto = db.PRODUCTOes
                        .Where(p => p.idProducto == productoId)
                        .Select(p => new ProductDto
                        {
                            IdProducto = p.idProducto,
                            Stock = p.stock
                        })
                        .FirstOrDefault();

                    // Si el producto existe, retornamos su stock; si no, retornamos 0
                    return producto?.Stock ?? 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener stock del producto: " + ex.Message);
                return 0;
            }
        }

        public TipoProductListDto ObtenerTiposDeProducto()
        {
            TipoProductListDto productListDto = new TipoProductListDto();

            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    productListDto.Users = db.TIPO_PRODUCTO
                        .Select(t => new TipoProductoDto
                        {
                            IdTipoProducto = t.idTipoProducto,
                            Nombre = t.nombre,
                            Imagen = t.imagen
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return productListDto;
        }

        public ProductListDto ObtenerProductos()
        {
            ProductListDto productsListDto = new ProductListDto();

            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    productsListDto.Products = db.PRODUCTOes
                        .Select(p => new ProductDto
                        {
                            IdProducto = p.idProducto,
                            IdEstado = p.idEstado,
                            Nombre = p.nombre,
                            Precio = (float?)p.valorUnitario,
                            Stock = p.stock,
                            
                            IdSabor = p.idSabor,
                            Imagen = p.imagen,
                            IdTipoProducto = p.idTipoProducto
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener productos: " + ex.Message);
            }

            return productsListDto;
        }
        public ProductDto ObtenerProductoPorId(int productoId)
        {
            ProductDto productDto = null;

            try
            {
                using (var db = new CRACKEDEntities31())
                {
                    productDto = db.PRODUCTOes
                        .Where(p => p.idProducto == productoId)
                        .Select(p => new ProductDto
                        {
                            IdProducto = p.idProducto,
                            Nombre = p.nombre,
                            Precio = (float?)p.valorUnitario,
                            Stock = p.stock,
                            Imagen = p.imagen,
                            IdTipoProducto = p.idTipoProducto
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el producto: " + ex.Message);
            }

            return productDto;
        }

    }
}
