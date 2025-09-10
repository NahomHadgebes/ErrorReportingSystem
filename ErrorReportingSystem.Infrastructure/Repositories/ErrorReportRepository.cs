using ErrorReportingSystem.Domain.Entities;
using ErrorReportingSystem.Domain.Interfaces;
using ErrorReportingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ErrorReportingSystem.Infrastructure.Repositories;

public class ErrorReportRepository : IErrorReportRepository
{
    private readonly AppDbContext _context;

    public ErrorReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ErrorReport report)
    {
        await _context.ErrorReports.AddAsync(report);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.ErrorReports.FindAsync(id);
        if (entity is null) return false;

        _context.ErrorReports.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ErrorReport>> GetAllAsync()
    {
        return await _context.ErrorReports.ToListAsync();
    }

    public async Task<ErrorReport?> GetByIdAsync(Guid id)
    {
        return await _context.ErrorReports.FindAsync(id);
    }

    public async Task UpdateAsync(ErrorReport report)
    {
        _context.ErrorReports.Update(report);
        await _context.SaveChangesAsync();
    }
}
