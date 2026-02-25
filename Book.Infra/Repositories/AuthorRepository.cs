using Book.Application.Interfaces.Repositories;
using Book.Core.Common;
using Book.Core.Entites;
using Book.Core.Extensions;
using Book.Infra.Context;
using Book.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Book.Infra.Repositories
{
    public class AuthorRepository : RepositoryBase<AppDbContext, Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<PagedList<Author>> SearchPaged(int page, int pageSize)
        {
            return await DbSet.ToPagedListAsync(page, pageSize);
        }

        public async Task<Author?> GetByIdWithBooksAsync(int id)
        {
            return await DbSet
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await DbSet.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> HasBooksAsync(int id)
        {
            return await Db.Set<Core.Entites.Book>().AnyAsync(b => b.AuthorId == id);
        }
    }
}
