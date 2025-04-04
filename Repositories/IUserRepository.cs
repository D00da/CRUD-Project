using CRUD_Project.Models;
using CRUD_Project.DTOs;

namespace CRUD_Project.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUser(int id);
        Task<List<UserModel>> GetAllUsers();
        Task<List<TaskModel>> GetAllUserTasks(int id);
        Task<UserModel> AddUser(UserDTO user);
        Task<UserModel> UpdateUser(UserModel oldUser, UserDTO user);
        Task<bool> DeleteUser(UserModel task);
    }
}
