using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Dtos;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogpostsController : ControllerBase
{
    private readonly IBlogpostService _service;

    public BlogpostsController(IBlogpostService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BlogpostResponse>>> GetAllAsync(CancellationToken ct)
    {
        var blogposts = await _service.GetAllAsync(ct);
        return Ok(blogposts);
    }

    [HttpGet("{id}", Name = "GetBlogpost")]
    public async Task<ActionResult<BlogpostResponse>> GetByIdAsync(int id, CancellationToken ct)
    {
        var blogpost = await _service.GetByIdAsync(id, ct);
        if (blogpost is null)
        {
            return NotFound();
        }

        return Ok(blogpost);
    }

    [HttpPost]
    public async Task<ActionResult<BlogpostResponse>> CreateAsync(CreateBlogpostRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(request, ct);
        return CreatedAtRoute("GetBlogpost", new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BlogpostResponse>> UpdateAsync(int id, UpdateBlogpostRequest request, CancellationToken ct)
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