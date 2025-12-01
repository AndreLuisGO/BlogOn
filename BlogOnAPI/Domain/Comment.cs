using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogOnAPI.Domain;

public sealed class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int BlogPostId { get; set; }

    [ForeignKey(nameof(BlogPostId))]
    public BlogPost? BlogPost { get; set; }
}