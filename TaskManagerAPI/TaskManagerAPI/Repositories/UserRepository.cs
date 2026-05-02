using TaskManagerAPI.Data;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                _dbContext.User.Add(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
            }
            return false;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                return _dbContext.User.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by email");
            }
            return null;
        }
    }
}
