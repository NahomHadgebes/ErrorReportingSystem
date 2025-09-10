using ErrorReportingSystem.Domain.Entities;

namespace ErrorReportingSystem.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByErrorReportIdAsync(Guid errorReportId);
        Task<Comment> AddAsync(Comment comment);
    }
}



