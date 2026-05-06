using SkyVisionStore.Domain.Entities.Cart;
using SkyVisionStore.Domain.Models.Cart;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface ICartActions
    {
        List<CartItem> GetCartByUserId(int userId);
        CartItem AddToCart(AddToCartModel model);
        CartItem? UpdateCartItem(int userId, int productId, int quantity);
        bool RemoveFromCart(int userId, int productId);
        bool ClearCart(int userId);
    }
}