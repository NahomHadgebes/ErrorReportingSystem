using ErrorReportingSystem.Application.DTOs;
using ErrorReportingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorReportingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    // GET: api/comments/errorreport/{errorReportId}
    [HttpGet("errorreport/{errorReportId:guid}")]
    public async Task<IActionResult> GetByErrorReportId(Guid errorReportId)
    {
        var comments = await _service.GetByErrorReportIdAsync(errorReportId);
        return Ok(comments);
    }

    // POST: api/comments
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetByErrorReportId), new { errorReportId = created.ErrorReportId }, created);
    }
}
