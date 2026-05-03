using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IUserTaskService _userTaskService;
        public TaskController(IHttpContextHelper httpContextHelper, IUserTaskService userTaskService)
        {
            _httpContextHelper = httpContextHelper;
            _userTaskService = userTaskService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = await _httpContextHelper.GetUserId();
            var tasks = await _userTaskService.GetUserTask(userId);
            return Ok(tasks);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateTask(AddTaskDto task)
        {
            var userId = await _httpContextHelper.GetUserId();
            var tasks = await _userTaskService.Addtask(task, userId);
            return Ok(tasks);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            var userId = await _httpContextHelper.GetUserId();
            var tasks = await _userTaskService.DeleteUserTask(taskId, userId);
            return Ok(tasks);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(UserTaskDto task)
        {
            var userId = await _httpContextHelper.GetUserId();
            var tasks = await _userTaskService.UpdateTask(task, userId);
            return Ok(tasks);
        }
    }
}
