using SkyVisionStore.Domain.Models.Cart;

using CartItemEntity = SkyVisionStore.Domain.Entities.Cart.CartItem;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface ICartActions
    {
        List<CartItemEntity> GetCartByUserId(int userId);

        CartItemEntity? AddToCart(AddToCartModel model);

        CartItemEntity? UpdateCartItem(int userId, int productId, int quantity);

        bool RemoveFromCart(int userId, int productId);

        bool ClearCart(int userId);
    }
}
