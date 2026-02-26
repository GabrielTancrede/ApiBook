using Book.Core.Common;
using BookEntity = Book.Core.Entities.Book;

namespace Book.Application.Interfaces.Repositories
{
    public interface IBookRepository : IRepositoryBase<BookEntity>
    {
        Task<BookEntity?> GetByIdWithRelationsAsync(int id);
        Task<bool> ExistsByTitleAndAuthorAsync(string title, int authorId, int? excludeId = null);
        Task<PagedList<BookEntity>> SearchPaged(int page, int pageSize);
    }
}
