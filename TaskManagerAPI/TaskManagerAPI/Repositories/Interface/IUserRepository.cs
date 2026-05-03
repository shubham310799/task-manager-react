using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);

        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int userId);
    }
}
