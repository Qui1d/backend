using SkyVisionStore.BusinessLogic.Interface;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Core.Product
{
    public class ProductActions : IProductActions
    {
        private static readonly List<ProductEntity> _products = new();
        private static int _nextId = 1;

        public List<ProductEntity> GetAll()
        {
            return _products;
        }

        public ProductEntity? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public ProductEntity Create(ProductEntity product)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.UtcNow;

            _products.Add(product);

            return product;
        }

        public ProductEntity? Update(int id, ProductEntity updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            return existingProduct;
        }

        public bool Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            return true;
        }
    }
}