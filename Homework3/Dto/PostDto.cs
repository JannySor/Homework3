namespace Homework3.Dto;

//data transfer object

public class PostDto
{
    public required int Id { get; set; }
    public required string Text { get; set; }
    public required List<string> Comments { get; set; }
}