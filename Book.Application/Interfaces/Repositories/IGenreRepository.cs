using Book.Core.Common;
using Book.Core.Entities;

namespace Book.Application.Interfaces.Repositories
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
        Task<bool> HasBooksAsync(int id);
        Task<PagedList<Genre>> SearchPaged(int page, int pageSize, bool paged = true);
    }
}
