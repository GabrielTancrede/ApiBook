using Book.Core.Common;
using Book.Core.Entites;

namespace Book.Application.Interfaces.Repositories
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        Task<bool> ExistsAsync(int id);
        Task<bool> HasBooksAsync(int id);
        Task<PagedList<Genre>> SearchPaged(int page, int pageSize);
    }
}
