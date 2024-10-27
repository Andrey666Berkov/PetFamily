using Microsoft.EntityFrameworkCore;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

namespace PetFamily.Application.Extensions;

public static class QueryExtensions
{
    public static async Task<PageList<T>> ToPageList<T>(
        this IQueryable<T> source,
        int page, int pagesize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((page - 1) * pagesize)
            .Take(pagesize)
            .ToListAsync(cancellationToken);

        return new PageList<T>()
        {
            Items = items,
            TotalCount = totalCount,
            PageSize = pagesize,
            Page = page
        };
    }
}