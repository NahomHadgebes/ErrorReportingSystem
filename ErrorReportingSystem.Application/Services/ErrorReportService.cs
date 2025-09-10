using ErrorReportingSystem.Application.DTOs;
using ErrorReportingSystem.Application.Interfaces;
using ErrorReportingSystem.Domain.Entities;
using ErrorReportingSystem.Domain.Interfaces;

namespace ErrorReportingSystem.Application.Services;

public class ErrorReportService : IErrorReportService
{
    private readonly IErrorReportRepository _repository;

    public ErrorReportService(IErrorReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ErrorReportDto>> GetAllAsync()
    {
        var reports = await _repository.GetAllAsync();
        return reports.Select(r => new ErrorReportDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            Priority = r.Priority,
            Status = r.Status,
            CreatedByUsername = r.CreatedByUsername,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<ErrorReportDto?> GetByIdAsync(Guid id)
    {
        var r = await _repository.GetByIdAsync(id);
        if (r == null) return null;

        return new ErrorReportDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            Priority = r.Priority,
            Status = r.Status,
            CreatedByUsername = r.CreatedByUsername,
            CreatedAt = r.CreatedAt
        };
    }

    // Application/Services/ErrorReportService.cs
    public async Task<ErrorReportDto> CreateAsync(CreateErrorReportRequest req)
    {
        var entity = new ErrorReport
        {
            Id = Guid.NewGuid(),
            Title = req.Title.Trim(),
            Description = req.Description?.Trim() ?? string.Empty,
            Priority = req.Priority,
            Status = req.Status,
            CreatedByUsername = req.CreatedByUsername.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);

        return new ErrorReportDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Priority = entity.Priority,
            Status = entity.Status,
            CreatedByUsername = entity.CreatedByUsername,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateErrorReportRequest req)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Title = req.Title.Trim();
        entity.Description = (req.Description ?? string.Empty).Trim();
        entity.Priority = req.Priority;
        entity.Status = req.Status;

        await _repository.UpdateAsync(entity); // se not nedan om repo
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}

