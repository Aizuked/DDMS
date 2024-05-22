using Web.Viewmodels;

namespace Web.Miscellaneous.Extensions;

public static class IQueryableEtensions
{
    public static IQueryable<T> Filter<T>(
        this IQueryable<T> query,
        ListBaseFilter filter
    ) where T : class
    {
        return
            query
                .Skip(filter.PageSize * filter.CurrentPage)
                .Take(filter.PageSize);
    }
}