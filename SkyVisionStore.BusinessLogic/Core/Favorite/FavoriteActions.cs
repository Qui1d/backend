using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Favorite;
using FavoriteEntity = SkyVisionStore.Domain.Entities.User.UserFavorite;

namespace SkyVisionStore.BusinessLogic.Core.Favorite
{
    public class FavoriteActions : IFavoriteActions
    {
        private static readonly List<FavoriteEntity> _favorites = new();
        private static int _nextId = 1;

        public List<FavoriteInfoModel> GetAll()
        {
            return _favorites.Select(ToInfoModel).ToList();
        }

        public FavoriteInfoModel? GetById(int id)
        {
            var favorite = _favorites.FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return null;
            }

            return ToInfoModel(favorite);
        }

        public FavoriteInfoModel Create(FavoriteCreateModel favorite)
        {
            var newFavorite = new FavoriteEntity
            {
                Id = _nextId++,
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                AddedAt = DateTime.UtcNow
            };

            _favorites.Add(newFavorite);

            return ToInfoModel(newFavorite);
        }

        public FavoriteInfoModel? Update(int id, FavoriteUpdateModel updatedFavorite)
        {
            var existingFavorite = _favorites.FirstOrDefault(f => f.Id == id);

            if (existingFavorite == null)
            {
                return null;
            }

            existingFavorite.UserId = updatedFavorite.UserId;
            existingFavorite.ProductId = updatedFavorite.ProductId;

            return ToInfoModel(existingFavorite);
        }

        public bool Delete(int id)
        {
            var favorite = _favorites.FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return false;
            }

            _favorites.Remove(favorite);

            return true;
        }

        public List<FavoriteInfoModel> GetFavorites(int userId)
        {
            return _favorites
                .Where(f => f.UserId == userId)
                .Select(ToInfoModel)
                .ToList();
        }

        public FavoriteInfoModel? AddToFavorites(int userId, int productId)
        {
            var existingFavorite = _favorites.FirstOrDefault(f =>
                f.UserId == userId &&
                f.ProductId == productId);

            if (existingFavorite != null)
            {
                return null;
            }

            var newFavorite = new FavoriteEntity
            {
                Id = _nextId++,
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.UtcNow
            };

            _favorites.Add(newFavorite);

            return ToInfoModel(newFavorite);
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

        private static FavoriteInfoModel ToInfoModel(FavoriteEntity favorite)
        {
            return new FavoriteInfoModel
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                AddedAt = favorite.AddedAt
            };
        }
    }
}