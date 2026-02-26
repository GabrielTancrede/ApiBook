using Book.Application.Interfaces.Repositories;
using Book.Core.Common;
using Book.Core.Extensions;
using Book.Infra.Context;
using Book.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Core.Entities.Book;

namespace Book.Infra.Repositories
{
    public class BookRepository : RepositoryBase<AppDbContext, BookEntity>, IBookRepository
    {
        public BookRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<BookEntity?> GetByIdWithRelationsAsync(int id)
        {
            return await DbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsByTitleAndAuthorAsync(string title, int authorId, int? excludeId = null)
        {
            return await DbSet.AnyAsync(b =>
                EF.Functions.ILike(b.Title, $"{title}") &&
                b.AuthorId == authorId &&
                (!excludeId.HasValue || b.Id != excludeId.Value));
        }

        public async Task<PagedList<BookEntity>> SearchPaged(int page, int pageSize, bool paged = true)
        {
            return await DbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsNoTracking()
                .ToPagedListAsync(page, pageSize, paged);
        }
    }
}
