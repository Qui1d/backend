using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.BusinessLogic.Interfaces
{
    public interface IUserBL
    {
        List<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        User? Update(int id, User updatedUser);
        bool Delete(int id);
    }
}
