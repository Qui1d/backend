using SkyVisionStore.Domain.Models.User;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface IUserActions
    {
        List<UserInfoModel> GetAll();

        UserInfoModel? GetById(int id);

        UserInfoModel? GetByEmailAndPassword(string email, string password);

        bool ExistsByEmail(string email);

        UserInfoModel Create(UserCreateModel user);

        UserInfoModel? Update(int id, UserUpdateModel updatedUser);

        bool Delete(int id);
    }
}