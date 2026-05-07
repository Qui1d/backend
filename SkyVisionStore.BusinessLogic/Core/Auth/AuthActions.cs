using SkyVisionStore.BusinessLogic.Core.User;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Auth;
using SkyVisionStore.Domain.Models.User;

namespace SkyVisionStore.BusinessLogic.Core.Auth
{
    public class AuthActions : IAuthActions
    {
        private readonly UserActions _userActions = new();

        public UserInfoModel? Login(UserLoginData loginData)
        {
            return _userActions.GetByEmailAndPassword(
                loginData.Email,
                loginData.Password);
        }

        public UserInfoModel? Register(UserRegisterData registerData)
        {
            var existingUser = _userActions.ExistsByEmail(registerData.Email);

            if (existingUser)
            {
                return null;
            }

            var newUser = new UserCreateModel
            {
                Username = registerData.Username,
                Email = registerData.Email,
                Password = registerData.Password
            };

            return _userActions.Create(newUser);
        }
    }
}