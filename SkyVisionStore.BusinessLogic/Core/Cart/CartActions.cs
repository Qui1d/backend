using Microsoft.EntityFrameworkCore;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Entities.Cart;

namespace SkyVisionStore.BusinessLogic.Core.Cart
{
    public class CartActions : ICartActions
    {
        public List<CartItem> GetCartByUserId(int userId)
        {
            using var db = new SkyVisionStoreContext();

            return db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Id)
                .ToList();
        }

        public CartItem? AddToCart(int userId, int productId, int quantity)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == userId);
            var productExists = db.Products.Any(p => p.Id == productId);

            if (!userExists || !productExists)
            {
                return null;
            }

            var normalizedQuantity = quantity <= 0 ? 1 : quantity;

            var existingItem = db.CartItems
                .Include(c => c.Product)
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += normalizedQuantity;
                db.SaveChanges();

                return existingItem;
            }

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = normalizedQuantity,
                AddedAt = DateTime.UtcNow
            };

            db.CartItems.Add(cartItem);
            db.SaveChanges();

            db.Entry(cartItem)
                .Reference(c => c.Product)
                .Load();

            return cartItem;
        }

        public CartItem? UpdateCartItem(int userId, int productId, int quantity)
        {
            using var db = new SkyVisionStoreContext();

            var item = db.CartItems
                .Include(c => c.Product)
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (item == null)
            {
                return null;
            }

            item.Quantity = quantity <= 0 ? 1 : quantity;
            db.SaveChanges();

            return item;
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