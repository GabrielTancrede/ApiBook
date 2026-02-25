using Book.Core.Common;
using Book.Core.Entites;

namespace Book.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        Task<Author?> GetByIdWithBooksAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasBooksAsync(int id);

        Task<PagedList<Author>> SearchPaged(int page, int pageSize);
    }
}
