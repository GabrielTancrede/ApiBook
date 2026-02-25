using Book.Application.Interfaces.Repositories;
using Book.Core.Common;
using Book.Core.Extensions;
using Book.Infra.Context;
using Book.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Core.Entites.Book;

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

        public async Task<PagedList<BookEntity>> SearchPaged(int page, int pageSize)
        {
            return await DbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsNoTracking()
                .ToPagedListAsync(page, pageSize);
        }

        public async Task<List<BookEntity>> GetByAuthorIdAsync(int authorId)
        {
            return await DbSet
                .Include(b => b.Genre)
                .Where(b => b.AuthorId == authorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<BookEntity>> GetByGenreIdAsync(int genreId)
        {
            return await DbSet
                .Include(b => b.Author)
                .Where(b => b.GenreId == genreId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await DbSet.AnyAsync(b => b.Id == id);
        }
    }
}
