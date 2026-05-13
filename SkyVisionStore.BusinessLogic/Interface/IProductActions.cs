using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IProductActions
    {
        List<ProductEntity> GetAll();

        ProductEntity? GetById(int id);

        ProductEntity? GetBySlug(string slug);

        ProductEntity Create(ProductEntity product);

        ProductEntity? Update(int id, ProductEntity updatedProduct);

        bool Delete(int id);
    }
}