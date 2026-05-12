using SkyVisionStore.Domain.Models.Product;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IProductActions
    {
        List<ProductEntity> GetAll();

        ProductEntity? GetById(int id);

        ProductEntity Create(ProductCreateModel product);

        ProductEntity? Update(int id, ProductUpdateModel updatedProduct);

        bool Delete(int id);
    }
}