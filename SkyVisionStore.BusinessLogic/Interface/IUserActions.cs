using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IUserActions
    {
        List<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        User? Update(int id, User updatedUser);
        bool Delete(int id);
    }
}