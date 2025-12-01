using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BlogOnAPI.Features.Posts;


public record CommentDto(int Id, string Text, DateTime CreatedAt);
public record PostDetailDto(int Id, string Title, string Content, DateTime CreatedAt, List<CommentDto> Comments);

public record GetPostByIdQuery(int Id) : IRequest<PostDetailDto?>;

// Internal classes for Dapper mapping
internal class PostDapperDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

internal class CommentDapperDto
{
    public int CommentId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CommentCreatedAt { get; set; }
}

public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDetailDto?>
{
    private readonly string _connectionString;

    public GetPostByIdHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<PostDetailDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = @"
            SELECT 
                p.Id, p.Title, p.Content, p.CreatedAt,
                c.Id AS CommentId, c.Text, c.CreatedAt AS CommentCreatedAt
            FROM BlogPosts p
            LEFT JOIN Comments c ON p.Id = c.BlogPostId
            WHERE p.Id = @Id";

        var postDictionary = new Dictionary<int, (PostDapperDto Post, List<CommentDto> Comments)>();

        var result = await connection.QueryAsync<PostDapperDto, CommentDapperDto, PostDapperDto>(
            sql,
            (post, commentDto) =>
            {
                if (!postDictionary.TryGetValue(post.Id, out var entry))
                {
                    entry = (post, new List<CommentDto>());
                    postDictionary.Add(post.Id, entry);
                }

                if (commentDto != null && commentDto.CommentId > 0)
                {
                    var comment = new CommentDto(commentDto.CommentId, commentDto.Text, commentDto.CommentCreatedAt);
                    entry.Comments.Add(comment);
                }

                return post;
            },
            new { request.Id },
            splitOn: "CommentId"
        );

        if (!postDictionary.Any())
            return null;

        var firstEntry = postDictionary.First().Value;
        return new PostDetailDto(
            firstEntry.Post.Id,
            firstEntry.Post.Title,
            firstEntry.Post.Content,
            firstEntry.Post.CreatedAt,
            firstEntry.Comments
        );
    }
}