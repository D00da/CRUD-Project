using Microsoft.AspNetCore.Mvc;
using CRUD_Project.Models;
using CRUD_Project.Repositories;
using CRUD_Project.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Components.Forms;

namespace CRUD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskServices;
    public TaskController(ITaskRepository taskServices)
    {
        _taskServices = taskServices;
    }

    [HttpGet]
    [Route("{id}", Name = "GetTask")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            var task = await _taskServices.GetTask(id);
            if (task == null)
            {
                return NotFound(new { message = $"No task found with the id: {id}" });
            }
            return Ok(new { message = "Successfully retrieved all blog posts", data = task });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving target task", error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        try
        {
            var taskList = await _taskServices.GetAllTasks();
            return Ok(new { message = "Successfully retrieved all blog posts", data = taskList });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving all tasks", error = ex.Message });
        }
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompletedTasks()
    {
        try
        {
            var taskList = await _taskServices.GetCompletedTasks();
            return Ok(new { message = "Successfully retrieved all blog posts", data = taskList });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving all completed task", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(TaskCreateDTO task)
    {
        if (task.title == null) //Title validation
        {
            return BadRequest(new { message = "Title is null" });
        }
        if (task.dateLimit < DateTime.Today) //Date Limit validation
        {
            return BadRequest(new { message = "Date Limit was set to before the current date" });
        }

        try
        {
            var newTask = await _taskServices.AddTask(task);
            return CreatedAtAction("GetTask", new { id = newTask.Id }, task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the new task", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(TaskUpdateDTO task, int id)
    {
        if (task.title == null) //Title validation
        {
            return BadRequest(new { message = "Title is null" });
        }
        if (task.dateLimit < DateTime.Today) // Date Limit validation
        {
            return BadRequest(new { message = "End date is set before the current date" });
        }
        
        var oldTask = await _taskServices.GetTask(id);

        if (oldTask == null) //ID validation
        {
            return NotFound(new { message = $"No task found with the id: {id}" });
        }
        if (task.status == oldTask.status || (int)task.status > 1) //State validation; can't be the same as previous
        {
            return BadRequest(new { message = "Invalid status defininition" });
        }

        try
        {
            var newTask = await _taskServices.UpdateTask(oldTask, task);
            return Ok(new { message = "Blog post successfully created", data=newTask}); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the target task", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            var targetTask = await _taskServices.GetTask(id);
            if (targetTask == null)
            {
                return NotFound(new { message = $"No task found with the id: {id}" });
            }
            var isDeleted = await _taskServices.DeleteTask(targetTask);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Failed to delete task with the id: {id}" });
            }
            return NoContent();

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the target task", error = ex.Message });
        }
    }
}
