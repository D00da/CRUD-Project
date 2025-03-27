using Microsoft.AspNetCore.Mvc;
using CRUD_Project.Models;
using CRUD_Project.Repositories;

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
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var todo = await _taskServices.GetAllTasks();
            if (todo == null || !todo.Any())
            {
                return Ok(new { message = "No Todo Items  found" });
            }
            return Ok(new { message = "Successfully retrieved all blog posts", data = todo });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


        }
    }
}
