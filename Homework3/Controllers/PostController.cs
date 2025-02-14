using Microsoft.AspNetCore.Mvc;
using Homework3.Repositories;
using Homework3.Repositories.Models;
using Microsoft.VisualBasic;

namespace Homework3.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PostController: ControllerBase
{
    [HttpGet("getPosts")]
    public IActionResult getPosts()
    {
        using var db = new DBRepository();

        var posts = db.Set<Post>()
            .Join(db.Set<User>(), 
                post => post.UserId, 
                user => user.Id, 
                (post, user) => new 
                {
                    PostId = post.Id,
                    Title = post.Title,
                    Text = post.Text,
                    CreatedAt = post.CreatedAt,
                    UserId = user.Id,
                    UserName = user.Name
                })
            .ToList();
        if (!posts.Any())
        {
            return NotFound(new { message = "Посты не найдены" });
        }
        
        return Ok(posts);
    }
    [HttpGet("getPostsByUserId/{userId}")]
    public IActionResult getPostsByUserId(int userId)
    {
        using var db = new DBRepository();
        if (!db.Set<User>().Any(u => u.Id == userId))
        {
            return NotFound(new { message = "Пользователь не найден" });
        }
        var posts = db.Set<Post>()
            .Where(p => p.UserId == userId)
            .Join(db.Set<User>(), 
                post => post.UserId, 
                user => user.Id, 
                (post, user) => new 
                {
                    PostId = post.Id,
                    Title = post.Title,
                    Text = post.Text,
                    CreatedAt = post.CreatedAt,
                    UserId = user.Id,
                    UserName = user.Name
                })
            .ToList();
        if (!posts.Any())
        {
            return NotFound(new { message = "У этого пользователя нет постов" });
        }
        return Ok(posts);
    }
    [HttpPost("addPost")]
    public IActionResult addPost(int userId, string title, string text)
    {
        using var db = new DBRepository();
        if (!db.Set<User>().Any(u => u.Id == userId))
        {
            return NotFound(new { message = "Пользователь не найден" });
        }
        db.Set<Post>().Add(new Post() {UserId = userId, Title = title, Text = text});
        db.SaveChanges();
        
        return Ok(new {message = "Пост добавлен"});
    }
    [HttpDelete("deletePost")]
    public IActionResult deletePost(int postId)
    {
        using var db = new DBRepository();
        var post = db.Set<Post>().FirstOrDefault(p => p.Id == postId);
        if (post is null)
        {
            return NotFound(new { message = "Пост не найден" });
        }
        db.Set<Post>().Remove(post);
        db.SaveChanges();
        
        return Ok(new {message = "Пост удален"});
    }
    [HttpPut("updatePost")]
    public IActionResult updatePost(int postId, string newTitle, string newText)
    {
        using var db = new DBRepository();
        var post = db.Set<Post>().FirstOrDefault(p => p.Id == postId);
        if (post is null)
        {
            return NotFound(new { message = "Пост не найден" });
        }
        post.Title = newTitle;
        post.Text = newText;
        db.SaveChanges();
        
        return Ok(new {message = "Пост обновлен"});
    }
}