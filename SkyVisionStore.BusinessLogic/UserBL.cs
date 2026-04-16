using SkyVisionStore.BusinessLogic.Interfaces;
using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.BusinessLogic
{
    public class UserBL : IUserBL
    {
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User Create(User user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;

            _users.Add(user);

            return user;
        }

        public User? Update(int id, User updatedUser)
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