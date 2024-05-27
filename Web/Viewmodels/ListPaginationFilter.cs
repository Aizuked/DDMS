namespace Web.Viewmodels;

public class ListPaginationFilter
{
    public int PageSize { get; set; } = 20;

    public int CurrentPage { get; set; }

    public int PageCount
    {
        get => _pageCount;
        set => _pageCount = (int)Math.Ceiling((double)value / PageSize);
    }

    private int _pageCount;
}