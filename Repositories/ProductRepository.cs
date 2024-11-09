using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRACKED.Repositories
{
    public class ProductRepository
    {
        public TipoProductListDto ObtenerTiposDeProducto()
        {
            TipoProductListDto productListDto = new TipoProductListDto();

            try
            {
                using (var db = new CRACKEDEntities25())
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
                // Manejo de errores (puedes añadir un log para ver más detalles)
                Console.WriteLine(ex.Message);
            }

            return productListDto;
        }
    

    public ProductListDto ObtenerProductos()
    {
        ProductListDto productsListDto = new ProductListDto();

        try
        {
            using (var db = new CRACKEDEntities25())
            {
                productsListDto.Products = db.PRODUCTOes
                    .Select(p => new ProductDto
                    {
                        IdProducto = p.idProducto,
                        IdEstado = p.idEstado,
                        Nombre = p.nombre,

                        Precio = (float?)p.valorUnitario,
                        Stock = p.stock,
                        Porcion = p.porcion,
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
    }
}

