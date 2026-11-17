using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public sealed class ProjectService : IProjectService
{
    private readonly ProjectDb _db;
    public ProjectService(ProjectDb db) => _db = db;

    private static ProjectResponse ToResponse(Project p) =>
        new(p.Id, p.Titel, p.Beschrijving, p.Categorie, p.GitHubUrl, p.Datum);

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct)
    {
        var projects = await _db.Projects.AsNoTracking().OrderBy(p => p.Id).ToListAsync(ct);
        return projects.Select(ToResponse).ToList();
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        var project = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
        return project is null ? null : ToResponse(project);
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var project = new Project
        {
            Titel = request.Titel,
            Beschrijving = request.Beschrijving,
            Categorie = request.Categorie,
            GitHubUrl = request.GitHubUrl,
            Datum = request.Datum
        };
        _db.Projects.Add(project);
        await _db.SaveChangesAsync(ct);
        return ToResponse(project);
    }

    public async Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (project is null)
        {
            return null;
        }

        project.Titel = request.Titel;
        project.Beschrijving = request.Beschrijving;
        project.Categorie = request.Categorie;
        project.GitHubUrl = request.GitHubUrl;
        project.Datum = request.Datum;

        await _db.SaveChangesAsync(ct);
        return ToResponse(project);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (project is null)
        {
            return false;
        }

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}