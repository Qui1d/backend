using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.BusinessLogic.Core.Favorite
{
    public class FavoriteActions : IFavoriteActions
    {
        private static readonly List<UserFavorite> _favorites = new();
        private static int _nextId = 1;

        public List<UserFavorite> GetFavorites(int userId)
        {
            return _favorites.Where(f => f.UserId == userId).ToList();
        }

        public UserFavorite AddToFavorites(int userId, int productId)
        {
            var existingFavorite = _favorites.FirstOrDefault(f =>
                f.UserId == userId &&
                f.ProductId == productId);

            if (existingFavorite != null)
            {
                return existingFavorite;
            }

            var favorite = new UserFavorite
            {
                Id = _nextId++,
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.UtcNow
            };

            _favorites.Add(favorite);
            return favorite;
        }

        public bool RemoveFromFavorites(int userId, int productId)
        {
            var favorite = _favorites.FirstOrDefault(f =>
                f.UserId == userId &&
                f.ProductId == productId);

            if (favorite == null)
            {
                return false;
            }

            _favorites.Remove(favorite);
            return true;
        }
    }
}