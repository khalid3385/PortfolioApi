namespace PortfolioApi.Dtos;

   public sealed class CreateBlogpostRequest {
        public string Titel { get; init; } = string.Empty;
        public string Inhoud { get; init; } = string.Empty;
        public DateTime Publicatiedatum { get; init; }
    }