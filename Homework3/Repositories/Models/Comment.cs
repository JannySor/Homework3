using System.ComponentModel.DataAnnotations.Schema; 

namespace Homework3.Repositories.Models;
public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public int PostId { get; set; }
    
    [ForeignKey("PostId")]
    public Post Post { get; set; }
    
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
}