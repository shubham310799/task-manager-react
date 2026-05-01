using TaskManagerAPI.Data;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        public UnitOfWork(AppDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
            }
            return false;
        }
    }
}
