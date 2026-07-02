namespace IHaveAnIdea.Entity.Concrete;

public class Post : Abstract.IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
    public string AppUserId { get; set; } = string.Empty;

    public Category Category { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}
