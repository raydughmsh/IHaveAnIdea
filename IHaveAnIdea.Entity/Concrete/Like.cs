namespace IHaveAnIdea.Entity.Concrete;

public class Like : Abstract.IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PostId { get; set; }
    public string AppUserId { get; set; } = string.Empty;

    public Post Post { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}
