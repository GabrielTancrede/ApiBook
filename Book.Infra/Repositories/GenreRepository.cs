using Book.Application.Interfaces.Repositories;
using Book.Core.Entites;
using Book.Infra.Context;
using Book.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Book.Infra.Repositories
{
    public class GenreRepository : RepositoryBase<AppDbContext, Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Genre?> GetByIdWithBooksAsync(int id)
        {
            return await DbSet
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await DbSet.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> HasBooksAsync(int id)
        {
            return await Db.Set<Core.Entites.Book>().AnyAsync(b => b.GenreId == id);
        }
    }
}
