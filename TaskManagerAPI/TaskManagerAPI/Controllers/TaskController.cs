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

        [HttpPost("add")]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            // Logic to create a new task in the database
            await Task.Delay(1000);
            var max = tasks?.Any() == true ? tasks.Max(t => t.Id) + 1 : 1;

            task.Id = max;
            tasks.Add(task);
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            await Task.Delay(1000);
            if (task != null) tasks.Remove(task);
            return Ok(tasks);
        }
    }
}
