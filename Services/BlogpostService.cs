using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public sealed class BlogpostService : IBlogpostService
{
    private readonly ProjectDb _db;
    public BlogpostService(ProjectDb db) => _db = db;

    private static BlogpostResponse ToResponse(Blogpost b) =>
        new(b.Id, b.Titel, b.Inhoud, b.Publicatiedatum);

    public async Task<IReadOnlyList<BlogpostResponse>> GetAllAsync(CancellationToken ct)
    {
        var blogposts = await _db.Blogposts.AsNoTracking().OrderBy(b => b.Id).ToListAsync(ct);
        return blogposts.Select(ToResponse).ToList();
    }

    public async Task<BlogpostResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        var blogpost = await _db.Blogposts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, ct);
        return blogpost is null ? null : ToResponse(blogpost);
    }

    public async Task<BlogpostResponse> CreateAsync(CreateBlogpostRequest request, CancellationToken ct)
    {
        var blogpost = new Blogpost
        {
            Titel = request.Titel,
            Inhoud = request.Inhoud,
            Publicatiedatum = request.Publicatiedatum
        };
        _db.Blogposts.Add(blogpost);
        await _db.SaveChangesAsync(ct);
        return ToResponse(blogpost);
    }

    public async Task<BlogpostResponse?> UpdateAsync(int id, UpdateBlogpostRequest request, CancellationToken ct)
    {
        var blogpost = await _db.Blogposts.FirstOrDefaultAsync(b => b.Id == id, ct);
        if (blogpost is null)
        {
            return null;
        }

        blogpost.Titel = request.Titel;
        blogpost.Inhoud = request.Inhoud;
        blogpost.Publicatiedatum = request.Publicatiedatum;

        await _db.SaveChangesAsync(ct);
        return ToResponse(blogpost);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var blogpost = await _db.Blogposts.FirstOrDefaultAsync(b => b.Id == id, ct);
        if (blogpost is null)
        {
            return false;
        }

        _db.Blogposts.Remove(blogpost);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}