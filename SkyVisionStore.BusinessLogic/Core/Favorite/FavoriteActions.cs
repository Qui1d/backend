using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Models.Favorite;
using FavoriteEntity = SkyVisionStore.Domain.Entities.Refs.UserFavorite;

namespace SkyVisionStore.BusinessLogic.Core.Favorite
{
    public class FavoriteActions : IFavoriteActions
    {
        public List<FavoriteInfoModel> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            return db.UserFavorites
                .OrderBy(f => f.Id)
                .Select(f => ToInfoModel(f))
                .ToList();
        }

        public FavoriteInfoModel? GetById(int id)
        {
            using var db = new SkyVisionStoreContext();

            var favorite = db.UserFavorites
                .FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return null;
            }

            return ToInfoModel(favorite);
        }

        public FavoriteInfoModel Create(FavoriteCreateModel favorite)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == favorite.UserId);
            var productExists = db.Products.Any(p => p.Id == favorite.ProductId);

            if (!userExists || !productExists)
            {
                throw new InvalidOperationException("User or product not found.");
            }

            var existingFavorite = db.UserFavorites
                .FirstOrDefault(f =>
                    f.UserId == favorite.UserId &&
                    f.ProductId == favorite.ProductId
                );

            if (existingFavorite != null)
            {
                return ToInfoModel(existingFavorite);
            }

            var newFavorite = new FavoriteEntity
            {
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                AddedAt = DateTime.UtcNow
            };

            db.UserFavorites.Add(newFavorite);
            db.SaveChanges();

            return ToInfoModel(newFavorite);
        }

        public FavoriteInfoModel? Update(int id, FavoriteUpdateModel updatedFavorite)
        {
            using var db = new SkyVisionStoreContext();

            var favorite = db.UserFavorites
                .FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return null;
            }

            var userExists = db.Users.Any(u => u.Id == updatedFavorite.UserId);
            var productExists = db.Products.Any(p => p.Id == updatedFavorite.ProductId);

            if (!userExists || !productExists)
            {
                return null;
            }

            var duplicateFavorite = db.UserFavorites
                .FirstOrDefault(f =>
                    f.Id != id &&
                    f.UserId == updatedFavorite.UserId &&
                    f.ProductId == updatedFavorite.ProductId
                );

            if (duplicateFavorite != null)
            {
                return null;
            }

            favorite.UserId = updatedFavorite.UserId;
            favorite.ProductId = updatedFavorite.ProductId;
            favorite.AddedAt = DateTime.UtcNow;

            db.SaveChanges();

            return ToInfoModel(favorite);
        }

        public bool Delete(int id)
        {
            using var db = new SkyVisionStoreContext();

            var favorite = db.UserFavorites
                .FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return false;
            }

            db.UserFavorites.Remove(favorite);
            db.SaveChanges();

            return true;
        }

        public List<FavoriteInfoModel> GetFavorites(int userId)
        {
            using var db = new SkyVisionStoreContext();

            return db.UserFavorites
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Id)
                .Select(f => ToInfoModel(f))
                .ToList();
        }

        public FavoriteInfoModel? AddToFavorites(int userId, int productId)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == userId);
            var productExists = db.Products.Any(p => p.Id == productId);

            if (!userExists || !productExists)
            {
                return null;
            }

            var existingFavorite = db.UserFavorites
                .FirstOrDefault(f =>
                    f.UserId == userId &&
                    f.ProductId == productId
                );

            if (existingFavorite != null)
            {
                return ToInfoModel(existingFavorite);
            }

            var favorite = new FavoriteEntity
            {
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.UtcNow
            };

            db.UserFavorites.Add(favorite);
            db.SaveChanges();

            return ToInfoModel(favorite);
        }

        public bool RemoveFromFavorites(int userId, int productId)
        {
            using var db = new SkyVisionStoreContext();

            var favorite = db.UserFavorites
                .FirstOrDefault(f =>
                    f.UserId == userId &&
                    f.ProductId == productId
                );

            if (favorite == null)
            {
                return false;
            }

            db.UserFavorites.Remove(favorite);
            db.SaveChanges();

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