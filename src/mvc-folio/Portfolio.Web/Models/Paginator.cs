namespace Portfolio.Web.Models;

public class Paginator<T>
{
	public Paginator() { }

	public Paginator(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
	}

	public IReadOnlyCollection<T>? Data { get; set; }

	public Paginator(IReadOnlyCollection<T> data)
	{
		Data = data;
	}

	public int? TotalCount { get; set; }
	public int PageSize { get; }
	public int PageNumber { get; }

	public string? SortColumn { get; set; }
	public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
