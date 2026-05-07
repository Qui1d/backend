using SkyVisionStore.Domain.Models.Product;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IProductActions
    {
        List<ProductInfoModel> GetAll();

        ProductInfoModel? GetById(int id);

        ProductInfoModel Create(ProductCreateModel product);

        ProductInfoModel? Update(int id, ProductUpdateModel updatedProduct);

        bool Delete(int id);
    }
}