using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Linq;

namespace CRACKED.Repositories
{
    public class ProductRepository
    {
        public bool GuardarProducto(PRODUCTO producto)
        {
            try
            {
                using (var db = new CRACKEDEntities35()) // Usando el contexto de tu base de datos
                {
                    db.PRODUCTOes.Add(producto); // Agregar el producto a la base de datos
                    db.SaveChanges(); // Guardar cambios
                    return true; // Si todo fue exitoso, retorna true
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías loguear el error si tienes un sistema de logs
                throw new Exception($"Error al guardar el producto: {ex.Message}");
            }
        }

        // Método para obtener el stock de un producto específico utilizando DTO
        public int ObtenerStockProducto(int productoId)
        {
            try
            {
                using (var db = new CRACKEDEntities35())
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
                using (var db = new CRACKEDEntities35())
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
        public ProductListDto ObtenerProductosAdmin()
        {
            ProductListDto productsListDto = new ProductListDto();
            try
            {
                using (var db = new CRACKEDEntities35())  // Asegúrate de usar el nombre de tu contexto
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
                            IdTipoProducto = p.idTipoProducto,
                            NombreSabor = p.SABOR != null ? p.SABOR.nombre : "", // Nombre del sabor
                            NombreTipoProducto = p.TIPO_PRODUCTO != null ? p.TIPO_PRODUCTO.nombre : "", // Nombre del tipo de producto
                            NombreEstado = p.ESTADO != null ? p.ESTADO.nombre : "" // Nombre del estado
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener productos: " + ex.Message);
            }

            return productsListDto;
        }

        public ProductListDto ObtenerProductos()
        {
            ProductListDto productsListDto = new ProductListDto();

            try
            {
                using (var db = new CRACKEDEntities35())
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
                using (var db = new CRACKEDEntities35())
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
        public bool ReducirStockProducto(int productoId, int cantidad)
        {
            try
            {
                using (var db = new CRACKEDEntities35())
                {
                    // Buscamos el producto en la base de datos
                    var producto = db.PRODUCTOes.FirstOrDefault(p => p.idProducto == productoId);

                    if (producto == null)
                    {
                        Console.WriteLine("Producto no encontrado.");
                        return false;
                    }

                    // Verificamos si hay suficiente stock
                    if (producto.stock < cantidad)
                    {
                        Console.WriteLine("Stock insuficiente para el producto.");
                    }
                    // Reducimos el stock
                    producto.stock -= cantidad;

                    // Guardamos los cambios en la base de datos
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reducir el stock del producto: " + ex.Message);
                return false;
            }
        }
        public ProductDto ObtenerProductoPorIdADMIN(int id)
        {
            using (var db = new CRACKEDEntities35())
            {
                var producto = db.PRODUCTOes
                .Where(p => p.idProducto == id)
                .Select(p => new ProductDto
                {
                    IdProducto = p.idProducto,
                    Nombre = p.nombre,
                    Precio = (float?)p.valorUnitario,
                    Stock = p.stock,
                    IdTipoProducto = p.idTipoProducto
                }).FirstOrDefault();

                return producto;
            }
        }

        // Actualizar un producto
        public bool ActualizarProducto(ProductDto productDto)
        {
            using (var db = new CRACKEDEntities35())
            {
                var productoExistente = db.PRODUCTOes.FirstOrDefault(p => p.idProducto == productDto.IdProducto);

                if (productoExistente == null)
                {
                    return false;
                }

                productoExistente.nombre = productDto.Nombre;
                productoExistente.valorUnitario = productDto.Precio;
                productoExistente.stock = productDto.Stock;
                productoExistente.idTipoProducto = productDto.IdTipoProducto;

                db.SaveChanges();
                return true;
            }
        }
        public bool DeleteProduct(int idProducto)
        {
            using (var db = new CRACKEDEntities35())
            {
                var producto = db.PRODUCTOes.FirstOrDefault(p => p.idProducto == idProducto);

                if (producto == null)
                {
                    return false; // Producto no encontrado
                }

                db.PRODUCTOes.Remove(producto);
                db.SaveChanges();
                return true;
            }
        }
    }
        
}
