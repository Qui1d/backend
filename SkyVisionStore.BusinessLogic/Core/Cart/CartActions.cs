using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Entities.Cart;
using SkyVisionStore.Domain.Models.Cart;

namespace SkyVisionStore.BusinessLogic.Core.Cart
{
    public class CartActions : ICartActions
    {
        private static readonly List<CartItem> _cartItems = new();
        private static int _nextId = 1;

        public List<CartItem> GetCartByUserId(int userId)
        {
            return _cartItems.Where(c => c.UserId == userId).ToList();
        }

        public CartItem AddToCart(AddToCartModel model)
        {
            var existingItem = _cartItems.FirstOrDefault(c =>
                c.UserId == model.UserId &&
                c.ProductId == model.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
                return existingItem;
            }

            var cartItem = new CartItem
            {
                Id = _nextId++,
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                AddedAt = DateTime.UtcNow
            };

            _cartItems.Add(cartItem);
            return cartItem;
        }

        public CartItem? UpdateCartItem(int userId, int productId, int quantity)
        {
            var item = _cartItems.FirstOrDefault(c =>
                c.UserId == userId &&
                c.ProductId == productId);

            if (item == null)
            {
                return null;
            }

            item.Quantity = quantity;
            return item;
        }

        public bool RemoveFromCart(int userId, int productId)
        {
            var item = _cartItems.FirstOrDefault(c =>
                c.UserId == userId &&
                c.ProductId == productId);

            if (item == null)
            {
                return false;
            }

            _cartItems.Remove(item);
            return true;
        }

        public bool ClearCart(int userId)
        {
            var items = _cartItems.Where(c => c.UserId == userId).ToList();

            if (!items.Any())
            {
                return false;
            }

            foreach (var item in items)
            {
                _cartItems.Remove(item);
            }

            return true;
        }
    }
}