using BlogOnAPI.Domain;
using BlogOnAPI.Infrastructure;
using FluentValidation;
using MediatR;

namespace BlogOnAPI.Features.Posts;

public record CreatePostCommand(string Title, string Content) : IRequest<int>;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}

public class CreatePostHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly BlogDbContext _context;

    public CreatePostHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new BlogPost
        {
            Title = request.Title,
            Content = request.Content,
        };

        _context.BlogPosts.Add(post);

        await _context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}