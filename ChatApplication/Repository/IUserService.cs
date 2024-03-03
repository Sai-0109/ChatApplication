using ChatApplication.Models;

namespace ChatApplication.Repository
{
    public interface IUserRepository
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int userId);

        void EditUser(UserModel user);
        void DeleteUser(int userId);
    }

}
