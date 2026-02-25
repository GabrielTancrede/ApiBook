namespace Book.Core.Common;

public class PagedList<TModel>
{
    private List<TModel> _data;

    public int? PreviousPage { get; internal set; }

    public int? NextPage { get; set; }

    public int CurrentPage { get; set; }

    public bool IsLast { get; internal set; }

    public int TotalPages { get; set; }

    public int TotalItens { get; set; }

    public int TotalItemsPage { get; set; }

    public int MaxItemsPerPage { get; internal set; } = 50;

    public bool Paged { get; internal set; }

    public List<TModel> Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            CalculatePageValues(TotalItens, _data?.Count ?? 0);
        }
    }

    public PagedList(List<TModel> items, int totalCount, int pageNumber, int pageSize, bool paged)
    {
        MaxItemsPerPage = pageSize;
        CurrentPage = pageNumber;
        Paged = paged;
        Data = new List<TModel>();
        if (items != null)
        {
            Data.AddRange(items);
        }

        CalculatePageValues(totalCount, items?.Count ?? 0);
    }

    public PagedList()
    {
    }

    private void CalculatePageValues(int totalCount, int dataCount)
    {
        TotalItemsPage = ((TotalItemsPage <= 0) ? dataCount : TotalItemsPage);
        TotalItens = ((TotalItens <= 0) ? totalCount : TotalItens);
        TotalPages = ((TotalPages > 0) ? TotalPages : ((MaxItemsPerPage > 0) ? ((int)Math.Ceiling((double)TotalItens / (double)MaxItemsPerPage)) : 0));
        NextPage = ((NextPage.HasValue && !(NextPage <= 0)) ? NextPage : ((CurrentPage >= TotalPages) ? null : new int?(CurrentPage + 1)));
        PreviousPage = ((PreviousPage.HasValue && !(PreviousPage <= 0)) ? PreviousPage : ((CurrentPage <= 1) ? null : new int?(CurrentPage - 1)));
        IsLast = CurrentPage >= TotalPages;
    }
}
