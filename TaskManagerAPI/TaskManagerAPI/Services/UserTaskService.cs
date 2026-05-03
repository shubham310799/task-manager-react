using TaskManagerAPI.Common;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUserService _userService;
        private readonly ILogger<UserTaskService> _logger;
        private readonly IUnitOfWork _uow;

        public UserTaskService(ITaskRepository taskRepo, ILogger<UserTaskService> logger, IUnitOfWork uow, IUserService userService)
        {
            _taskRepo = taskRepo;
            _logger = logger;
            _uow = uow;
            _userService = userService;
        }
        public async Task<GlobalResponseDto<IList<UserTaskDto>>> Addtask(AddTaskDto taskDto, string userId)
        {
            GlobalResponseDto<IList<UserTaskDto>> res = new();
            try
            {
                var user = await GetUserById(userId);
                if (user != null)
                {
                    var taskToAdd = new UserTask
                    {
                        Name = taskDto.Name,
                        Description = taskDto.Description,
                        DueDate = taskDto.DueDate,
                        Status = taskDto.Status,
                        UserId = user.UserId
                    };
                    await _taskRepo.AddUserTask(taskToAdd);
                    await _uow.SaveChangesAsync();
                    var tasks = await _taskRepo.GetUserTasks(user.UserId);
                    res.Data = await GetUserTasksByUserId(user.UserId);
                }
                else
                {
                    res.Error = ErrorCodes.SomethingWentWrong;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        public async Task<GlobalResponseDto<IList<UserTaskDto>>> GetUserTask(string userId)
        {
            GlobalResponseDto<IList<UserTaskDto>> res = new();
            try
            {
                if (Int32.TryParse(userId, out int userIdInt))
                {
                    var tasks = await _taskRepo.GetUserTasks(userIdInt);
                    res.Data = await GetUserTasksByUserId(userIdInt);
                }
                else
                {
                    res.Error = ErrorCodes.SomethingWentWrong;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        public async Task<GlobalResponseDto<IList<UserTaskDto>>> DeleteUserTask(string taskId, string userId)
        {
            GlobalResponseDto<IList<UserTaskDto>> res = new();
            try
            {
                if (Int32.TryParse(taskId, out int taskIdInt) && Int32.TryParse(userId, out int userIdInt))
                {
                    await _taskRepo.DeleteUserTasks(taskIdInt, userIdInt);
                    await _uow.SaveChangesAsync();
                    var updatedTasks = await _taskRepo.GetUserTasks(userIdInt);
                    res.Data = await GetUserTasksByUserId(userIdInt);
                }
                else
                {
                    res.Error = ErrorCodes.SomethingWentWrong;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        public async Task<GlobalResponseDto<IList<UserTaskDto>>> UpdateTask(UserTaskDto task, string userId)
        {
            GlobalResponseDto<IList<UserTaskDto>> res = new();
            try
            {
                var userIdInt = Int32.Parse(userId);
                var currentTask = await _taskRepo.GetTaskByUserIdAndTaskId(userIdInt, task.Id);
                if (currentTask != null)
                {
                    currentTask.Status = task.Status;
                    currentTask.Name = task.Name;
                    currentTask.Description = task.Description;
                    currentTask.Status = task.Status;
                    await _uow.SaveChangesAsync();
                    res.Data = await GetUserTasksByUserId(userIdInt);
                }
                else
                {
                    res.Error = ErrorCodes.InvalidTask;
                }
            }
            catch (Exception ex)
            {
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        private async Task<IList<UserTaskDto>> GetUserTasksByUserId(int userId)
        {
            try
            {
                var tasks = await _taskRepo.GetUserTasks(userId);
                return tasks.Select(ut =>
                {
                    return new UserTaskDto
                    {
                        Id = ut.UserTaskId,
                        Name = ut.Name,
                        Description = ut.Description,
                        DueDate = ut.DueDate,
                        Status = ut.Status
                    };
                }).ToList();
            }
            catch
            {
                return new List<UserTaskDto>();
            }
        }

        private async Task<User?> GetUserById(string userId)
        {
            try
            {
                return (await _userService.GetUserById(userId))?.Data;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                return null;
            }
        }
    }
}
