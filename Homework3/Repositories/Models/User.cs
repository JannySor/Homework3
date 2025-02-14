namespace Homework3.Repositories.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}