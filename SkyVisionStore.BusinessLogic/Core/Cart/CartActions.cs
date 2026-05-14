using Microsoft.EntityFrameworkCore;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Models.Cart;

using CartItemEntity = SkyVisionStore.Domain.Entities.Cart.CartItem;

namespace SkyVisionStore.BusinessLogic.Core.Cart
{
    public class CartActions : ICartActions
    {
        public List<CartItemEntity> GetCartByUserId(int userId)
        {
            using var db = new SkyVisionStoreContext();

            return db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Id)
                .ToList();
        }

        public CartItemEntity? AddToCart(AddToCartModel model)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == model.UserId);
            var productExists = db.Products.Any(p => p.Id == model.ProductId);

            if (!userExists || !productExists)
            {
                return null;
            }

            var quantity = model.Quantity <= 0 ? 1 : model.Quantity;

            var existingItem = db.CartItems
                .FirstOrDefault(c => c.UserId == model.UserId && c.ProductId == model.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                db.SaveChanges();

                return db.CartItems
                    .Include(c => c.Product)
                    .FirstOrDefault(c => c.Id == existingItem.Id);
            }

            var cartItem = new CartItemEntity
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = quantity,
                AddedAt = DateTime.UtcNow
            };

            db.CartItems.Add(cartItem);
            db.SaveChanges();

            return db.CartItems
                .Include(c => c.Product)
                .FirstOrDefault(c => c.Id == cartItem.Id);
        }

        public CartItemEntity? UpdateCartItem(int userId, int productId, int quantity)
        {
            using var db = new SkyVisionStoreContext();

            var item = db.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (item == null)
            {
                return null;
            }

            item.Quantity = quantity <= 0 ? 1 : quantity;
            db.SaveChanges();

            return db.CartItems
                .Include(c => c.Product)
                .FirstOrDefault(c => c.Id == item.Id);
        }

        public bool RemoveFromCart(int userId, int productId)
        {
            using var db = new SkyVisionStoreContext();

            var item = db.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (item == null)
            {
                return false;
            }

            db.CartItems.Remove(item);
            db.SaveChanges();

            return true;
        }

        public bool ClearCart(int userId)
        {
            using var db = new SkyVisionStoreContext();

            var items = db.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            if (!items.Any())
            {
                return false;
            }

            db.CartItems.RemoveRange(items);
            db.SaveChanges();

            return true;
        }
    }
}