using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<IList<UserTask>> GetUserTasks(int userId);
        Task AddUserTask(UserTask userTask);
        Task DeleteUserTasks(int taskId, int userId);
        Task<UserTask?> GetTaskByUserIdAndTaskId(int userId, int taskId);
    }
}
