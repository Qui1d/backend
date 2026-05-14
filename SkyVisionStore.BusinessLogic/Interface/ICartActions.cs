using SkyVisionStore.Domain.Entities.Cart;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface ICartActions
    {
        List<CartItem> GetCartByUserId(int userId);

        CartItem? AddToCart(int userId, int productId, int quantity);

        CartItem? UpdateCartItem(int userId, int productId, int quantity);

        bool RemoveFromCart(int userId, int productId);

        bool ClearCart(int userId);
    }
}