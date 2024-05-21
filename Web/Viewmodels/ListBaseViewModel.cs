namespace Web.Viewmodels;

public class ListBaseViewModel
{
    public int PageSize { get; set; } = 20;

    public int PageCount { get; set; }

    public int CurrentPage { get; set; }
}