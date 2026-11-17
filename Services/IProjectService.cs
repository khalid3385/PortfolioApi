using PortfolioApi.Dtos;

namespace PortfolioApi.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct);
    Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}