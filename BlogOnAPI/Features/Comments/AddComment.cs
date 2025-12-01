using BlogOnAPI.Domain;
using BlogOnAPI.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogOnAPI.Features.Comments;

public record CreatedCommentDto(int Id, string Text, DateTime CreatedAt);

public record AddCommentCommand(int PostId, string Text) : IRequest<CreatedCommentDto?>;

public class AddCommentValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Comment text cannot be empty")
            .MaximumLength(1000).WithMessage("Comment is too long");
    }
}

public class AddCommentHandler : IRequestHandler<AddCommentCommand, CreatedCommentDto?>
{
    private readonly BlogDbContext _context;

    public AddCommentHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<CreatedCommentDto?> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var postExists = await _context.BlogPosts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists) return null; 

        var comment = new Comment
        {
            BlogPostId = request.PostId,
            Text = request.Text
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreatedCommentDto(comment.Id, comment.Text, comment.CreatedAt);
    }
}