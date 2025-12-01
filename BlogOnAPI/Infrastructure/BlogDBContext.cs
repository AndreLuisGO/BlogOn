using Microsoft.EntityFrameworkCore;
using BlogOnAPI.Domain;

namespace BlogOnAPI.Infrastructure;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // We use Fluent API here to seed data. 
        // This ensures that when you run 'dotnet ef database update', 
        // this data is automatically inserted.

        modelBuilder.Entity<BlogPost>().HasData(
            new BlogPost
            {
                Id = 1,
                Title = "Welcome to the Clean Architecture Blog",
                Content = "This is a sample post demonstrating CQRS and Vertical Slices.",
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            },
            new BlogPost
            {
                Id = 2,
                Title = "Understanding MediatR and CQRS Pattern",
                Content = "In this post, we explore how MediatR enables the CQRS pattern by separating read and write operations. This separation allows us to optimize each operation independently and maintain cleaner code architecture.",
                CreatedAt = new DateTime(2024, 1, 5, 10, 30, 0, DateTimeKind.Utc)
            },
            new BlogPost
            {
                Id = 3,
                Title = "Redis Caching Strategies for ASP.NET Core",
                Content = "Learn how to implement efficient caching strategies using Redis in ASP.NET Core applications. We'll cover cache invalidation, distributed caching, and performance optimization techniques.",
                CreatedAt = new DateTime(2024, 1, 10, 14, 15, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Comment>().HasData(
            // Comments for Post 1
            new Comment
            {
                Id = 1,
                BlogPostId = 1,
                Text = "Great architecture choice!",
                CreatedAt = new DateTime(2024, 1, 1, 12, 30, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 2,
                BlogPostId = 1,
                Text = "I've been looking for a good example of vertical slice architecture. Thanks for sharing!",
                CreatedAt = new DateTime(2024, 1, 1, 13, 15, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 3,
                BlogPostId = 1,
                Text = "How does this compare to traditional layered architecture?",
                CreatedAt = new DateTime(2024, 1, 1, 15, 45, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 4,
                BlogPostId = 1,
                Text = "Clean and simple implementation. Would love to see more posts like this!",
                CreatedAt = new DateTime(2024, 1, 2, 9, 20, 0, DateTimeKind.Utc)
            },
            // Comments for Post 2
            new Comment
            {
                Id = 5,
                BlogPostId = 2,
                Text = "MediatR has really simplified my projects. Great explanation!",
                CreatedAt = new DateTime(2024, 1, 5, 11, 10, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 6,
                BlogPostId = 2,
                Text = "Could you elaborate more on the performance implications of using MediatR?",
                CreatedAt = new DateTime(2024, 1, 5, 14, 25, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 7,
                BlogPostId = 2,
                Text = "The separation of concerns is fantastic. Makes unit testing so much easier.",
                CreatedAt = new DateTime(2024, 1, 6, 8, 50, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 8,
                BlogPostId = 2,
                Text = "Do you recommend using the pipeline behaviors for cross-cutting concerns?",
                CreatedAt = new DateTime(2024, 1, 6, 16, 30, 0, DateTimeKind.Utc)
            },
            // Comments for Post 3
            new Comment
            {
                Id = 9,
                BlogPostId = 3,
                Text = "Redis has been a game-changer for our application performance!",
                CreatedAt = new DateTime(2024, 1, 10, 15, 5, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 10,
                BlogPostId = 3,
                Text = "What's your strategy for cache invalidation in a microservices environment?",
                CreatedAt = new DateTime(2024, 1, 10, 17, 40, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 11,
                BlogPostId = 3,
                Text = "Very helpful! I was struggling with distributed caching before reading this.",
                CreatedAt = new DateTime(2024, 1, 11, 10, 15, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 12,
                BlogPostId = 3,
                Text = "Would be great to see a follow-up on Redis clustering and high availability.",
                CreatedAt = new DateTime(2024, 1, 11, 13, 55, 0, DateTimeKind.Utc)
            }
        );
    }
}