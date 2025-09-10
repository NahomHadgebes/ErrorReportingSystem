using ErrorReportingSystem.Application.DTOs;

namespace ErrorReportingSystem.Application.Interfaces;

public interface IErrorReportService
{
    Task<ErrorReportDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ErrorReportDto>> GetAllAsync();
    Task<ErrorReportDto> CreateAsync(CreateErrorReportRequest req); // ändrad här

    Task<bool> UpdateAsync(Guid id, UpdateErrorReportRequest req);
    Task<bool> DeleteAsync(Guid id);
}