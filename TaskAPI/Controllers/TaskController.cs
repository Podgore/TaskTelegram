using Microsoft.AspNetCore.Mvc;
using TaskAPI.Abstractions.Services;
using TaskAPI.Common.DTO;

namespace TaskAPI.Controllers
{
    [Route ("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("tasks")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _taskService.GetTask();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("task/{Id}")]
        public async Task<IActionResult> GetTaskByName(string Name)
        {
            return Ok(await _taskService.GetTaskByName(Name));
        }

        [HttpPost("task")]
        public async Task<IActionResult> InsertTask(TaskDTO task)
        {
            try
            {
                var result = await _taskService.AddTask(task);
                return CreatedAtAction(nameof(GetTaskByName), new {id = result}, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("task/{Id}")]
        public async Task<IActionResult> UpdateTask(string Name, [FromBody] UpdateTaskDTO task)
        {
            try
            {
                return Ok(await _taskService.UpdateDate(Name, task));
            }
            catch(KeyNotFoundException)
            {
                return NotFound(Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("task/{Id}")]
        public async Task<IActionResult> DeleteTask(string Name)
        {
            return await _taskService.DeleteTask(Name) ? Ok() : NotFound();
        }
    }
}
