using ErrorReportingSystem.Domain.Entities;
using ErrorReportingSystem.Domain.Interfaces;
using ErrorReportingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ErrorReportingSystem.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _db;

        public CommentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Comment>> GetByErrorReportIdAsync(Guid errorReportId)
        {
            return await _db.Comments
                .Where(c => c.ErrorReportId == errorReportId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return comment;
        }
    }
}


