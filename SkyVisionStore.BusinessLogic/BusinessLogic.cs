using SkyVisionStore.BusinessLogic.Interfaces;

namespace SkyVisionStore.BusinessLogic
{
    public class BusinessLogic
    {
        public IUserBL GetUserBL()
        {
            return new UserBL();
        }
    }
}