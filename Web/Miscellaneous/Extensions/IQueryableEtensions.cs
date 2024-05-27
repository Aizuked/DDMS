using Web.Viewmodels;

namespace Web.Miscellaneous.Extensions;

public static class IQueryableEtensions
{
    public static IQueryable<T> Paginate<T, LPF>(
        this IQueryable<T> query,
        LPF paginationFilter
    ) where T : class
      where LPF : ListPaginationFilter
    {
        return
            query
                .Skip(paginationFilter.PageSize * paginationFilter.CurrentPage)
                .Take(paginationFilter.PageSize);
    }
}