using UserEntity = SkyVisionStore.Domain.Entities.User.User;
using SkyVisionStore.Domain.Models.Auth;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IAuthActions
    {
        UserEntity? Login(UserLoginData loginData);
        UserEntity? Register(UserRegisterData registerData);
    }
}