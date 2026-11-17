using PortfolioApi.Dtos;

namespace PortfolioApi.Services;

public interface IBlogpostService
{
    Task<IReadOnlyList<BlogpostResponse>> GetAllAsync(CancellationToken ct);
    Task<BlogpostResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<BlogpostResponse> CreateAsync(CreateBlogpostRequest request, CancellationToken ct);
    Task<BlogpostResponse?> UpdateAsync(int id, UpdateBlogpostRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}