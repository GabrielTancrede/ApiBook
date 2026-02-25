using Book.Core.Common;
using BookEntity = Book.Core.Entites.Book;

namespace Book.Application.Interfaces.Repositories
{
    public interface IBookRepository : IRepositoryBase<BookEntity>
    {
        Task<BookEntity?> GetByIdWithRelationsAsync(int id);
        Task<PagedList<BookEntity>> SearchPaged(int page, int pageSize);
    }
}
