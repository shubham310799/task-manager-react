using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        static readonly List<TaskModel> tasks = new List<TaskModel>();

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasks()
        {
            // Logic to retrieve all tasks from the database
            await Task.Delay(1000);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            // Logic to create a new task in the database
            await Task.Delay(1000);
            tasks.Add(task);
            return Ok(tasks);
        }
    }
}
