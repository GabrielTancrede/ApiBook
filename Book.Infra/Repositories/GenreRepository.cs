using Book.Application.Interfaces.Repositories;
using Book.Core.Common;
using Book.Core.Entities;
using Book.Core.Extensions;
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

        public async Task<PagedList<Genre>> SearchPaged(int page, int pageSize, bool paged = true)
        {
            return await DbSet.ToPagedListAsync(page, pageSize, paged);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await DbSet.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
        {
            return await DbSet.AnyAsync(g =>
                EF.Functions.ILike(g.Name, $"%{name}%") &&
                (!excludeId.HasValue || g.Id != excludeId.Value));
        }

        public async Task<bool> HasBooksAsync(int id)
        {
            return await Db.Set<Core.Entities.Book>().AnyAsync(b => b.GenreId == id);
        }
    }
}
