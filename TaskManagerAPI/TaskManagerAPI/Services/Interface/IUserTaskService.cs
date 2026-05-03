using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Services.Interface
{
    public interface IUserTaskService
    {
        Task<GlobalResponseDto<IList<UserTaskDto>>> Addtask(AddTaskDto task, string userId);
        Task<GlobalResponseDto<IList<UserTaskDto>>> GetUserTask(string userId);
        Task<GlobalResponseDto<IList<UserTaskDto>>> DeleteUserTask(string taskId, string userId);
        Task<GlobalResponseDto<IList<UserTaskDto>>> UpdateTask(UserTaskDto task, string userId);
    }
}
