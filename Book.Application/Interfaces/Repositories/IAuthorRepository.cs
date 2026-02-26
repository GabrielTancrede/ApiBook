using Book.Core.Common;
using Book.Core.Entities;

namespace Book.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
        Task<bool> HasBooksAsync(int id);
        Task<PagedList<Author>> SearchPaged(int page, int pageSize, bool paged = true);
    }
}
