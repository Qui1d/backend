using SkyVisionStore.BusinessLogic.Interface;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;

namespace SkyVisionStore.BusinessLogic.Core.User
{
    public class UserActions : IUserActions
    {
        private static readonly List<UserEntity> _users = new();
        private static int _nextId = 1;

        public List<UserEntity> GetAll()
        {
            return _users;
        }

        public UserEntity? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public UserEntity Create(UserEntity user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;

            _users.Add(user);

            return user;
        }

        public UserEntity? Update(int id, UserEntity updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            return existingUser;
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
    }
}