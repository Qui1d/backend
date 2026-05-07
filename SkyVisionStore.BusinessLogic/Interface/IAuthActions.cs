using SkyVisionStore.Domain.Models.Auth;
using SkyVisionStore.Domain.Models.User;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IAuthActions
    {
        UserInfoModel? Login(UserLoginData loginData);

        UserInfoModel? Register(UserRegisterData registerData);
    }
}