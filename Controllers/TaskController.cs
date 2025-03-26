using Microsoft.AspNetCore.Mvc;
using CRUD_Project.Models;

namespace CRUD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
    public IEnumerable<TaskModel> GetTask()
    {
        return Ok();
    }
}
