using ErrorReportingSystem.Application.DTOs;
using ErrorReportingSystem.Application.Interfaces;
using ErrorReportingSystem.Domain.Entities;
using ErrorReportingSystem.Domain.Interfaces;

namespace ErrorReportingSystem.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CommentDto>> GetByErrorReportIdAsync(Guid errorReportId)
        {
            var comments = await _repository.GetByErrorReportIdAsync(errorReportId);
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                ErrorReportId = c.ErrorReportId,
                Content = c.Content,
                CreatedByUsername = c.CreatedByUsername,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<CommentDto> CreateAsync(CreateCommentRequest request)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                ErrorReportId = request.ErrorReportId,
                Content = request.Content,
                CreatedByUsername = request.CreatedByUsername,
                CreatedAt = DateTime.UtcNow
            };

            var added = await _repository.AddAsync(comment);

            return new CommentDto
            {
                Id = added.Id,
                ErrorReportId = added.ErrorReportId,
                Content = added.Content,
                CreatedByUsername = added.CreatedByUsername,
                CreatedAt = added.CreatedAt
            };
        }
    }
}


