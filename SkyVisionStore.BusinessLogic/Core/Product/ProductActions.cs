using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Product;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Core.Product
{
    public class ProductActions : IProductActions
    {
        private static readonly List<ProductEntity> _products = new();
        private static int _nextId = 1;

        public List<ProductInfoModel> GetAll()
        {
            return _products.Select(ToInfoModel).ToList();
        }

        public ProductInfoModel? GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return ToInfoModel(product);
        }

        public ProductInfoModel Create(ProductCreateModel product)
        {
            var newProduct = new ProductEntity
            {
                Id = _nextId++,
                Title = product.Title,
                Slug = product.Slug,
                Platform = product.Platform,
                Genre = product.Genre,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Discount = product.Discount,
                Image = product.Image,
                RecommendedImage = product.RecommendedImage,
                Region = product.Region,
                Description = product.Description,
                Requirements = ConvertRequirementsToString(product.Requirements),
                IsNew = product.IsNew,
                IsPopular = product.IsPopular,
                IsUpcoming = product.IsUpcoming,
                CreatedAt = DateTime.UtcNow
            };

            _products.Add(newProduct);

            return ToInfoModel(newProduct);
        }

        public ProductInfoModel? Update(int id, ProductUpdateModel updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Title = updatedProduct.Title;
            existingProduct.Slug = updatedProduct.Slug;
            existingProduct.Platform = updatedProduct.Platform;
            existingProduct.Genre = updatedProduct.Genre;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.OldPrice = updatedProduct.OldPrice;
            existingProduct.Discount = updatedProduct.Discount;
            existingProduct.Image = updatedProduct.Image;
            existingProduct.RecommendedImage = updatedProduct.RecommendedImage;
            existingProduct.Region = updatedProduct.Region;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Requirements = ConvertRequirementsToString(updatedProduct.Requirements);
            existingProduct.IsNew = updatedProduct.IsNew;
            existingProduct.IsPopular = updatedProduct.IsPopular;
            existingProduct.IsUpcoming = updatedProduct.IsUpcoming;

            return ToInfoModel(existingProduct);
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

        private static ProductInfoModel ToInfoModel(ProductEntity product)
        {
            return new ProductInfoModel
            {
                Id = product.Id,
                Title = product.Title,
                Slug = product.Slug,
                Platform = product.Platform,
                Genre = product.Genre,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Discount = product.Discount,
                Image = product.Image,
                RecommendedImage = product.RecommendedImage,
                Region = product.Region,
                Description = product.Description,
                Requirements = ConvertRequirementsToArray(product.Requirements),
                IsNew = product.IsNew,
                IsPopular = product.IsPopular,
                IsUpcoming = product.IsUpcoming,
                CreatedAt = product.CreatedAt
            };
        }

        private static string ConvertRequirementsToString(string[] requirements)
        {
            if (requirements == null || requirements.Length == 0)
            {
                return string.Empty;
            }

            return string.Join("; ", requirements);
        }

        private static string[] ConvertRequirementsToArray(string requirements)
        {
            if (string.IsNullOrWhiteSpace(requirements))
            {
                return Array.Empty<string>();
            }

            return requirements
                .Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
    }
}