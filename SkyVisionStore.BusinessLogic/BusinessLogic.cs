using SkyVisionStore.BusinessLogic.Core.Auth;
using SkyVisionStore.BusinessLogic.Core.Cart;
using SkyVisionStore.BusinessLogic.Core.Coupon;
using SkyVisionStore.BusinessLogic.Core.Favorite;
using SkyVisionStore.BusinessLogic.Core.Order;
using SkyVisionStore.BusinessLogic.Core.Product;
using SkyVisionStore.BusinessLogic.Core.User;
using SkyVisionStore.BusinessLogic.Interface;

namespace SkyVisionStore.BusinessLogic
{
    public class BusinessLogic
    {
        public IAuthActions GetAuthActions()
        {
            return new AuthActions();
        }

        public IUserActions GetUserActions()
        {
            return new UserActions();
        }

        public IProductActions GetProductActions()
        {
            return new ProductActions();
        }

        public ICartActions GetCartActions()
        {
            return new CartActions();
        }

        public IFavoriteActions GetFavoriteActions()
        {
            return new FavoriteActions();
        }

        public IOrderActions GetOrderActions()
        {
            return new OrderActions();
        }

        public ICouponActions GetCouponActions()
        {
            return new CouponActions();
        }
    }
}