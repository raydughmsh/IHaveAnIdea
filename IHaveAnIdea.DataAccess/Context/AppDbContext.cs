using IHaveAnIdea.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IHaveAnIdea.DataAccess.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Unique constraint: a user can only like a post once
        builder.Entity<Like>()
            .HasIndex(l => new { l.PostId, l.AppUserId })
            .IsUnique();

        // Post → Category
        builder.Entity<Post>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Post → AppUser
        builder.Entity<Post>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment → Post
        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment → AppUser
        builder.Entity<Comment>()
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Like → Post
        builder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Like → AppUser
        builder.Entity<Like>()
            .HasOne(l => l.AppUser)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Seed categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fikir" },
            new Category { Id = 2, Name = "Öneri" },
            new Category { Id = 3, Name = "Şikayet" },
            new Category { Id = 4, Name = "Duygu" }
        );
    }
}
