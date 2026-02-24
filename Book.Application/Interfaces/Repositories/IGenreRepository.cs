using Book.Core.Entites;

namespace Book.Application.Interfaces.Repositories
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        Task<Genre?> GetByIdWithBooksAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasBooksAsync(int id);
    }
}
