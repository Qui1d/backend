using SkyVisionStore.BusinessLogic.Core.Auth;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Models.User;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;

namespace SkyVisionStore.BusinessLogic.Core.User
{
    public class UserActions : IUserActions
    {
        public List<UserInfoModel> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            return db.Users
                .OrderBy(user => user.Id)
                .Select(user => ToInfoModel(user))
                .ToList();
        }

        public UserInfoModel? GetById(int id)
        {
            using var db = new SkyVisionStoreContext();

            var user = db.Users
                .FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            return ToInfoModel(user);
        }

        public UserInfoModel? GetByEmailAndPassword(string email, string password)
        {
            using var db = new SkyVisionStoreContext();

            var user = db.Users
                .FirstOrDefault(user => user.Email == email);

            if (user == null)
            {
                return null;
            }

            var isHashedPassword = PasswordHasher.IsHashedPassword(user.Password);

            if (isHashedPassword)
            {
                var isPasswordValid = PasswordHasher.VerifyPassword(password, user.Password);

                if (!isPasswordValid)
                {
                    return null;
                }

                return ToInfoModel(user);
            }

            
            if (user.Password != password)
            {
                return null;
            }

            user.Password = PasswordHasher.HashPassword(password);
            db.SaveChanges();

            return ToInfoModel(user);
        }

        public bool ExistsByEmail(string email)
        {
            using var db = new SkyVisionStoreContext();

            return db.Users.Any(user => user.Email == email);
        }

        public UserInfoModel Create(UserCreateModel user)
        {
            using var db = new SkyVisionStoreContext();

            var newUser = new UserEntity
            {
                Username = user.Username,
                Email = user.Email,
                Password = PasswordHasher.HashPassword(user.Password),
                Role = user.Role,
                CreatedAt = DateTime.UtcNow
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            return ToInfoModel(newUser);
        }

        public UserInfoModel? Update(int id, UserUpdateModel updatedUser)
        {
            using var db = new SkyVisionStoreContext();

            var existingUser = db.Users
                .FirstOrDefault(user => user.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                existingUser.Password = PasswordHasher.HashPassword(updatedUser.Password);
            }

            existingUser.Role = updatedUser.Role;

            db.SaveChanges();

            return ToInfoModel(existingUser);
        }

        public bool Delete(int id)
        {
            using var db = new SkyVisionStoreContext();

            var user = db.Users
                .FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return false;
            }

            db.Users.Remove(user);
            db.SaveChanges();

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