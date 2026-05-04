using SkyVisionStore.BusinessLogic.Interfaces;
using SkyVisionStore.Domain.Entities.Product;

namespace SkyVisionStore.BusinessLogic
{
    public class ProductBL : IProductBL 
    {
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.UtcNow;

            _products.Add(product);

            return product;
        }

        public Product? Update(int id, Product updatedProduct)
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