using ErrorReportingSystem.Application.DTOs;

namespace ErrorReportingSystem.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetByErrorReportIdAsync(Guid errorReportId);
        Task<CommentDto> CreateAsync(CreateCommentRequest request);
    }
}

