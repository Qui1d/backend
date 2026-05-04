using SkyVisionStore.Domain.Entities.Product;

namespace SkyVisionStore.BusinessLogic.Interfaces
{
    public interface IProductBL
    {
        List<Product> GetAll();
        Product? GetById(int id);
        Product Create(Product product);
        Product? Update(int id, Product updatedProduct);
        bool Delete(int id);
    }
}
