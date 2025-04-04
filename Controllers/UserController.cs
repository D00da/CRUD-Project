using CRUD_Project.DTOs;
using CRUD_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userServices;
    public UserController(IUserRepository userServices)
    {
        _userServices = userServices;
    }

    [HttpGet]
    [Route("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userServices.GetUser(id);
            if (user == null)
            {
                return NotFound(new { message = $"No user found with the id: {id}" });
            }
            return Ok(new { message = "Successfully retrieved all users", data = user });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving target user", error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var userList = await _userServices.GetAllUsers();
            return Ok(new { message = "Successfully retrieved all users", data = userList });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving all users", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserDTO user)
    {
        if (user.name == null) //Name validation
        {
            return BadRequest(new { message = "Title is null" });
        }

        try
        {
            var newUser = await _userServices.AddUser(user);
            return CreatedAtAction("GetUser", new { id = newUser.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the new user", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(UserDTO user, int id)
    {
        if (user.name == null) //Name validation
        {
            return BadRequest(new { message = "Title is null" });
        }

        var oldUser = await _userServices.GetUser(id);

        if (oldUser == null) //ID validation
        {
            return NotFound(new { message = $"No user found with the id: {id}" });
        }

        try
        {
            var newUser = await _userServices.UpdateUser(oldUser, user);
            return Ok(new { message = "Blog post successfully created", data = newUser });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the target user", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var targetUser = await _userServices.GetUser(id);
            if (targetUser == null)
            {
                return NotFound(new { message = $"No user found with the id: {id}" });
            }
            var isDeleted = await _userServices.DeleteUser(targetUser);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Failed to delete user with the id: {id}" });
            }
            return NoContent();

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the target user", error = ex.Message });
        }
    }
}