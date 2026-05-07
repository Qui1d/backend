using SkyVisionStore.Domain.Models.Favorite;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IFavoriteActions
    {
        List<FavoriteInfoModel> GetAll();

        FavoriteInfoModel? GetById(int id);

        FavoriteInfoModel Create(FavoriteCreateModel favorite);

        FavoriteInfoModel? Update(int id, FavoriteUpdateModel updatedFavorite);

        bool Delete(int id);

        List<FavoriteInfoModel> GetFavorites(int userId);

        FavoriteInfoModel? AddToFavorites(int userId, int productId);

        bool RemoveFromFavorites(int userId, int productId);
    }
}