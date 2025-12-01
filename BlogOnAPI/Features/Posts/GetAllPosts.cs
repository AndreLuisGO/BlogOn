using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BlogOnAPI.Features.Posts;

public record PostSummaryDto(int Id, string Title, DateTime CreatedAt, int CommentCount);

public record GetAllPostsQuery : IRequest<IEnumerable<PostSummaryDto>>;

public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostSummaryDto>>
{
    private readonly string _connectionString;

    public GetAllPostsHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<PostSummaryDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = @"
            SELECT 
                p.Id, 
                p.Title, 
                p.CreatedAt,
                COUNT(c.Id) as CommentCount
            FROM BlogPosts p
            LEFT JOIN Comments c ON p.Id = c.BlogPostId
            GROUP BY p.Id, p.Title, p.CreatedAt
            ORDER BY p.CreatedAt DESC";

        return await connection.QueryAsync<PostSummaryDto>(sql);
    }
}