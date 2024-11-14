using CRACKED.Dtos;
using CRACKED.Models;
using CRACKED.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;

namespace CRACKED.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public TipoProductListDto ObtenerTiposDeProducto()
        {
            return _productRepository.ObtenerTiposDeProducto();
        }
        public ProductListDto ObtenerProductos()
        {
            //ProductRepository productRepository = new ProductRepository();
            return _productRepository.ObtenerProductos();
        }

        public ProductListDto ObtenerProductosPorTipo(int tipoProductoId)
        {
            // Obtener todos los productos
            ProductListDto todosLosProductos = _productRepository.ObtenerProductos();

            // Verificar que la lista de productos no sea nula
            if (todosLosProductos?.Products == null)
            {
                todosLosProductos = new ProductListDto { Products = new List<ProductDto>() };
            }

            // Filtrar los productos que tienen el IdTipoProducto especificado
            ProductListDto productosFiltrados = new ProductListDto
            {
                Products = todosLosProductos.Products
                    .Where(p => p.IdTipoProducto == tipoProductoId)
                    .ToList()
            };

            return productosFiltrados;
        }
        public ProductListDto ObtenerProductosPorId(int ProductoId)
        {
            // Obtener todos los productos
            ProductListDto todosLosProductos = _productRepository.ObtenerProductos();

            // Verificar que la lista de productos no sea nula
            if (todosLosProductos?.Products == null)
            {
                todosLosProductos = new ProductListDto { Products = new List<ProductDto>() };
            }

            // Filtrar los productos que tienen el IdTipoProducto especificado
            ProductListDto productosFiltrados = new ProductListDto
            {
                Products = todosLosProductos.Products
                    .Where(p => p.IdProducto == ProductoId)
                    .ToList()
            };

            return productosFiltrados;
        }
        public ProductListDto ObtenerProductosAdmin()
        {
            return _productRepository.ObtenerProductosAdmin();
        }
        public bool CrearProducto(ProductDto productDto)
        {
            try
            {
                // Mapeo del DTO a la entidad de la base de datos
                var producto = new PRODUCTO
                {
                    nombre = productDto.Nombre,
                    valorUnitario = productDto.Precio ?? 0,
                    stock = productDto.Stock,
                    idEstado = 1,
                    idSabor = 4,
                    idTipoProducto = productDto.IdTipoProducto,
                    imagen = null,
                };

                // Llamada al repositorio
                return _productRepository.GuardarProducto(producto);
            }
            catch (Exception ex)
            {
                // Si hay un error, lo propagamos
                throw new Exception($"Error en el servicio: {ex.Message}");
            }

        }
        public ProductDto ObtenerProductoPorId(int id)
        {
            return _productRepository.ObtenerProductoPorIdADMIN(id);
        }

        // Actualizar un producto
        public bool ActualizarProducto(ProductDto productDto)
        {
            return _productRepository.ActualizarProducto(productDto);
        }

        public bool DeleteProduct(int idProducto)
        {
            if (idProducto == 0)
            {
                throw new ArgumentException("ID del producto no válido");
            }

            var resultado = _productRepository.DeleteProduct(idProducto);

            if (!resultado)
            {
                throw new InvalidOperationException("No se pudo eliminar el producto. Puede que no exista.");
            }

            return true;
        }
    }
}

