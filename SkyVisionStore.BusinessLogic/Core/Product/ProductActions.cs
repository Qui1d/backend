using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;

using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Core.Product
{
    public class ProductActions : IProductActions
    {
        public List<ProductEntity> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            return db.Products
                .OrderBy(p => p.Id)
                .ToList();
        }

        public ProductEntity? GetById(int id)
        {
            using var db = new SkyVisionStoreContext();

            return db.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public ProductEntity? GetBySlug(string slug)
        {
            using var db = new SkyVisionStoreContext();

            var normalizedSlug = slug.Trim().ToLower();

            return db.Products
                .FirstOrDefault(p => p.Slug.ToLower() == normalizedSlug);
        }

        public ProductEntity Create(ProductEntity product)
        {
            using var db = new SkyVisionStoreContext();

            var newProduct = new ProductEntity
            {
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
                Requirements = product.Requirements,
                IsNew = product.IsNew,
                IsPopular = product.IsPopular,
                IsUpcoming = product.IsUpcoming,
                CreatedAt = DateTime.UtcNow
            };

            db.Products.Add(newProduct);
            db.SaveChanges();

            return newProduct;
        }

        public ProductEntity? Update(int id, ProductEntity updatedProduct)
        {
            using var db = new SkyVisionStoreContext();

            var existingProduct = db.Products
                .FirstOrDefault(p => p.Id == id);

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
            existingProduct.Requirements = updatedProduct.Requirements;
            existingProduct.IsNew = updatedProduct.IsNew;
            existingProduct.IsPopular = updatedProduct.IsPopular;
            existingProduct.IsUpcoming = updatedProduct.IsUpcoming;

            db.SaveChanges();

            return existingProduct;
        }

        public bool Delete(int id)
        {
            using var db = new SkyVisionStoreContext();

            var product = db.Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return true;
        }
    }
}