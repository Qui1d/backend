using SkyVisionStore.BusinessLogic.Core.User;
using SkyVisionStore.BusinessLogic.Interface;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;
using SkyVisionStore.Domain.Models.Auth;

namespace SkyVisionStore.BusinessLogic.Core.Auth
{
    public class AuthActions : IAuthActions
    {
        private readonly UserActions _userActions = new();

        public UserEntity? Login(UserLoginData loginData)
        {
            return _userActions.GetAll().FirstOrDefault(u =>
                u.Email == loginData.Email &&
                u.Password == loginData.Password);
        }

        public UserEntity? Register(UserRegisterData registerData)
        {
            var existingUser = _userActions.GetAll()
                .FirstOrDefault(u => u.Email == registerData.Email);

            if (existingUser != null)
            {
                return null;
            }

            var newUser = new UserEntity
            {
                Username = registerData.Username,
                Email = registerData.Email,
                Password = registerData.Password
            };

            return _userActions.Create(newUser);
        }
    }
}