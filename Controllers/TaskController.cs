using Microsoft.AspNetCore.Mvc;
using CRUD_Project.Models;
using CRUD_Project.Repositories;
using CRUD_Project.DTOs;
using System.Threading.Tasks;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            var taskId = await _taskServices.GetTask(id);
            if (taskId == null)
            {
                return StatusCode(404, new { message = $"No task found with the id: {id}" });
            }
            return StatusCode(200, new { message = "Successfully retrieved all blog posts", data = taskId });

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
            if (taskList == null || !taskList.Any())
            {
                return StatusCode(404, new { message = "No tasks found" });
            }
            return StatusCode(200, new { message = "Successfully retrieved all blog posts", data = taskList });

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
            if (taskList == null || !taskList.Any())
            {
                return StatusCode(404, new { message = "No tasks found" });
            }
            return StatusCode(200, new { message = "Successfully retrieved all blog posts", data = taskList });

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
            return StatusCode(400, new { message = "Title is null" });
        }
        if (task.dateLimit < DateTime.Today) //Date Limit validation
        {
            return StatusCode(400, new { message = "Date Limit was set to before the current date" });
        }
        try
        {
            await _taskServices.AddTask(task);
            //return CreatedAtAction(nameof(AddTask), new { id = .Id });
        
            return StatusCode(201, new { message = "Task successfully created" }); //TODO data = addedTask
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
            return StatusCode(400, new { message = "Title is null" });
        }
        if (task.dateLimit < DateTime.Today) //Date Limit validation
        {
            return StatusCode(400, new { message = "End date is set before the current date" });
        }
        var oldTask = await _taskServices.GetTask(id);
        if (oldTask == null) //ID validation
        {
            return StatusCode(404, new { message = $"No task found with the id: {id}" });
        }
        if (task.status == oldTask.status || (task.status != Enums.State.Unfinished && task.status != Enums.State.Finished)) //State validation; can't be the same
        {
            return StatusCode(400, new { message = "Invalid status defininition" });
        }
        try
        {
            var newTask = await _taskServices.UpdateTask(task, id);
            return StatusCode(200, new { message = "Blog post successfully created", data=newTask}); 

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
                return StatusCode(404, new { message = $"No task found with the id: {id}" });
            }
            await _taskServices.DeleteTask(id);
            return StatusCode(204, new { message = "Task removed sucessfully" });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the target task", error = ex.Message });
        }
    }
}
