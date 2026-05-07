using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.User;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;

namespace SkyVisionStore.BusinessLogic.Core.User
{
    public class UserActions : IUserActions
    {
        private static readonly List<UserEntity> _users = new();
        private static int _nextId = 1;

        public List<UserInfoModel> GetAll()
        {
            return _users.Select(ToInfoModel).ToList();
        }

        public UserInfoModel? GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return ToInfoModel(user);
        }

        public UserInfoModel? GetByEmailAndPassword(string email, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);

            if (user == null)
            {
                return null;
            }

            return ToInfoModel(user);
        }

        public bool ExistsByEmail(string email)
        {
            return _users.Any(u => u.Email == email);
        }

        public UserInfoModel Create(UserCreateModel user)
        {
            var newUser = new UserEntity
            {
                Id = _nextId++,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                CreatedAt = DateTime.UtcNow
            };

            _users.Add(newUser);

            return ToInfoModel(newUser);
        }

        public UserInfoModel? Update(int id, UserUpdateModel updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.Role = updatedUser.Role;

            return ToInfoModel(existingUser);
        }

        public bool Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            _users.Remove(user);

            return true;
        }

        private static UserInfoModel ToInfoModel(UserEntity user)
        {
            return new UserInfoModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
    }
}