using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IFavoriteActions
    {
        List<UserFavorite> GetFavorites(int userId);
        UserFavorite AddToFavorites(int userId, int productId);
        bool RemoveFromFavorites(int userId, int productId);
    }
}