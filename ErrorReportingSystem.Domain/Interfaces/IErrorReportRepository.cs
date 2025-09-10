using ErrorReportingSystem.Domain.Entities;

namespace ErrorReportingSystem.Domain.Interfaces;

public interface IErrorReportRepository
{
    Task<ErrorReport?> GetByIdAsync(Guid id);
    Task<IEnumerable<ErrorReport>> GetAllAsync();
    Task AddAsync(ErrorReport entity);
    Task UpdateAsync(ErrorReport entity);

    Task<bool> DeleteAsync(Guid id);
}


