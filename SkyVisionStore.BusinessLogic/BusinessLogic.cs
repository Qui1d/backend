using SkyVisionStore.BusinessLogic.Core.Product;
using SkyVisionStore.BusinessLogic.Core.User;
using SkyVisionStore.BusinessLogic.Interface;

namespace SkyVisionStore.BusinessLogic
{
    public class BusinessLogic
    {
        public IUserActions GetUserActions()
        {
            return new UserActions();
        }

        public IProductActions GetProductActions()
        {
            return new ProductActions();
        }
    }
}
