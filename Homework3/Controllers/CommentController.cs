using Homework3.Repositories;
using Homework3.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework3.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CommentController: ControllerBase
{
    [HttpGet("getCommentsByPostId/{postId}")]
    public IActionResult getCommentsByPostId(int postId)
    {
        using var db = new DBRepository();
        bool postExists = db.Set<Post>().Any(p => p.Id == postId);
        if (!postExists)
        {
            return NotFound(new { message = "Пост не найден" });
        }
        var comments = db.Set<Comment>()
            .Where(c => c.PostId == postId)
            .ToList();

        if (!comments.Any())
        {
            return NotFound(new { message = "Комментарии не найдены" });
        }

        return Ok(comments);
    }
    [HttpGet("getCommentsByUserId/{userId}")]
    public IActionResult getCommentsByUserId(int userId)
    {
        using var db = new DBRepository();
        bool userExists = db.Set<User>().Any(u => u.Id == userId);
        if (!userExists)
        {
            return NotFound(new { message = "Пользователь не найден" });
        }
        var comments = db.Set<Comment>()
            .Where(comment => comment.UserId == userId)
            .ToList(); 

        if (!comments.Any())
        {
            return NotFound(new { message = "Комментарии не найдены" });
        }

        return Ok(comments);
    }
    [HttpPost("addComment")]
    public IActionResult addComment(string newComment, int postId, int userId)
    {
        using var db = new DBRepository();
        bool userExists = db.Set<User>().Any(u => u.Id == userId);
        if (!userExists)
        {
            return NotFound(new { message = "Пользователь не найден" });
        }
        var post = db.Set<Post>().FirstOrDefault(p => p.Id == postId);
        if (post is null)
        {
            return NotFound(new { message = "Пост не найден" });
        }
        post.Comments.Add(new Comment() {Text = newComment, PostId = postId, UserId = userId});
        db.SaveChanges();
        
        return Ok(new { message = "Комментарий добавлен" });
    }
    [HttpDelete("deleteComment")]
    public IActionResult deleteComment(int commentId)
    {
        using var db = new DBRepository();
        var commentToDelete = db.Set<Comment>().FirstOrDefault(c => c.Id == commentId);
        if (commentToDelete is null)
        {
            return NotFound(new { message = "Комментарий не найден" });
        }
        db.Set<Comment>().Remove(commentToDelete);
        db.SaveChanges();
        
        return Ok();
    }
}