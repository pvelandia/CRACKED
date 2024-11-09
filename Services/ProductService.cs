using CRACKED.Dtos;
using CRACKED.Repositories;
using System.Collections.Generic;
using System.Linq;

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




    }
}
