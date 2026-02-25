using Book.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace Book.Core.Extensions;

public static class PagedListExtensions
{
    public static PagedList<TModel> ToPagedList<TModel>(this IQueryable<TModel> source, int pageNumber, int pageSize, bool paged = true)
    {
        int totalCount = source.Count();
        IQueryable<TModel> source2;
        if (!paged)
        {
            source2 = source;
        }
        else
        {
            source2 = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        return new PagedList<TModel>(source2.ToList(), totalCount, pageNumber, pageSize, paged);
    }

    public static async Task<PagedList<TModel>> ToPagedListAsync<TModel>(this IQueryable<TModel> source, int pageNumber, int pageSize, bool paged = true)
    {
        return new PagedList<TModel>(
            totalCount: await source.CountAsync(),
            items: await ((IQueryable<TModel>)(paged ? ((IQueryable)source.Skip((pageNumber - 1) * pageSize).Take(pageSize)) : ((IQueryable)source))).ToListAsync(),
            pageNumber: pageNumber,
            pageSize: pageSize,
            paged: paged);
    }
}
