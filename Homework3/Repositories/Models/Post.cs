using System.ComponentModel.DataAnnotations.Schema; 

namespace Homework3.Repositories.Models;
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<Comment> Comments { get; set; } = new();
}