using TaskManagerAPI.Data;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<TaskRepository> _logger;
        public TaskRepository(AppDbContext dbContext, ILogger<TaskRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task AddUserTask(UserTask userTask)
        {
            try
            {
                _dbContext.UserTask.Add(userTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding userTask for userId {userTask.UserId}");
                throw;
            }
        }

        public async Task DeleteUserTasks(int taskId, int userId)
        {
            try
            {
                _dbContext.UserTask.RemoveRange(_dbContext.UserTask.Where(ut => ut.UserTaskId == taskId && ut.UserId == userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting userTask with taskId {taskId} for userId {userId}");
                throw;
            }
        }

        public async Task<IList<UserTask>> GetUserTasks(int userId)
        {
            try
            {
                return _dbContext.UserTask.Where(ut => ut.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error in {nameof(GetUserTasks)}");
                throw;
            }
        }

        public async Task<UserTask?> GetTaskByUserIdAndTaskId(int userId, int taskId)
        {
            try
            {
                return _dbContext.UserTask.FirstOrDefault(ut => ut.UserId == userId && ut.UserTaskId == taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error in {nameof(GetTaskByUserIdAndTaskId)}");
                throw;
            }
        }
    }
}
