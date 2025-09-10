using ErrorReportingSystem.Application.DTOs;
using ErrorReportingSystem.Application.Interfaces; // justera om ditt interface ligger i annat namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorReportingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorReportsController : ControllerBase
{
    private readonly IErrorReportService _service;

    public ErrorReportsController(IErrorReportService service)
    {
        _service = service;
    }

    // Alla får läsa (ingen token krävs)
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _service.GetAllAsync();
        return Ok(reports);
    }

    // Alla får läsa (ingen token krävs)
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var report = await _service.GetByIdAsync(id);
        if (report == null) return NotFound();
        return Ok(report);
    }

    // Skapa kräver inlogg (User eller Admin)
    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateErrorReportRequest req)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var dto = await _service.CreateAsync(req);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // Uppdatera kräver inlogg (User eller Admin)
    [Authorize(Roles = "Admin,User")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateErrorReportRequest req)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var ok = await _service.UpdateAsync(id, req);
        return ok ? NoContent() : NotFound();
    }

    // Ta bort kräver Admin
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}


