using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Dtos;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectResponse>>> GetAllAsync(CancellationToken ct)
    {
        var projects = await _service.GetAllAsync(ct);
        return Ok(projects);
    }

    [HttpGet("{id}", Name = "GetProject")]
    public async Task<ActionResult<ProjectResponse>> GetByIdAsync(int id, CancellationToken ct)
    {
        var project = await _service.GetByIdAsync(id, ct);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> CreateAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(request, ct);
        return CreatedAtRoute("GetProject", new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponse>> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}