using Homework3.Repositories;
using Homework3.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework3.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController: ControllerBase
{
    [HttpGet("getUsers")]
    public IActionResult getUsers()
    {
        using var db = new DBRepository();
        var users = db.Set<User>().ToList();
        
        return Ok(users);
    }
    [HttpGet("getUserById/{userId}")]
    public IActionResult getUserById(int userId)
    {
        using var db = new DBRepository();
        var user = db.Set<User>().FirstOrDefault(user => userId == user.Id);
        
        return Ok(user);
    }
    [HttpPost("addUser")]
    public IActionResult addUser(string name, int age)
    {
        using var db = new DBRepository();
        User newUser = new User { Name = name, Age = age };
        db.Set<User>().Add(newUser);
        db.SaveChanges();
        
        return Ok();
    }
    [HttpDelete("deleteUser")]
    public IActionResult deleteUser(int userId)
    {
        using var db = new DBRepository();
        var userToDelete = db.Set<User>().FirstOrDefault(user => userId == user.Id);
        if (userToDelete is null)
        {
            return NotFound();
        }
        db.Set<User>().Remove(userToDelete);
        db.SaveChanges();
        
        return Ok();
    }
    [HttpPut("updateUser")]
    public IActionResult updateUser(int userId, string newName)
    {
        using var db = new DBRepository();
        var userToUpdate = db.Set<User>().FirstOrDefault(user => userId == user.Id);
        if (userToUpdate is null)
        {
            return NotFound();
        }
        userToUpdate.Name = newName;
        db.SaveChanges();
        
        return Ok();
    }
    
}