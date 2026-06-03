using SkyVisionStore.BusinessLogic.Core.Product;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Product;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Functions.Product
{
    public class ProductFlow : ProductActions, IProductActions
    {
        public new List<ProductEntity> GetAll()
        {
            return base.GetAll();
        }

        public new ProductEntity? GetById(int id)
        {
            return base.GetById(id);
        }

        public new ProductEntity? GetBySlug(string slug)
        {
            return base.GetBySlug(slug);
        }

        public new ProductEntity Create(ProductCreateModel product)
        {
            return base.Create(product);
        }

        public new ProductEntity? Update(int id, ProductUpdateModel updatedProduct)
        {
            return base.Update(id, updatedProduct);
        }

        public new bool Delete(int id)
        {
            return base.Delete(id);
        }
    }
}