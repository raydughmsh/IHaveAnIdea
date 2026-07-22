namespace IHaveAnIdea.Business.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AppUserId { get; set; } = string.Empty;
    public int PostId { get; set; }
}
