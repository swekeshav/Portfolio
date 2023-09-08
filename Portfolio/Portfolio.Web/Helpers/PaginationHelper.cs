using Portfolio.Web.Models;

namespace Portfolio.Web.Helpers;

public static class PaginationHelper
{
	public static Paginator<T> BuildPaginator<T>(HttpRequest request) where T : class
	{
		string? draw = request.Query["draw"].FirstOrDefault();

		if (string.IsNullOrWhiteSpace(draw))
		{
			return new Paginator<T>(int.MaxValue, 1);
		}

		string? start = request.Query["start"].FirstOrDefault();
		string? length = request.Query["length"].FirstOrDefault();

		string? firstColumn = request.Query["order[0][column]"].FirstOrDefault();
		string? sortColumn = request.Query["columns[" + firstColumn + "][name]"].FirstOrDefault();
		string? sortColumnDirection = request.Query["order[0][dir]"].FirstOrDefault();

		int pageSize = Math.Abs(length != null ? Convert.ToInt32(length) : 0);
		int pageNumber = CalculatePageNumber();

		return new Paginator<T>(pageNumber, pageSize)
		{
			SortColumn = sortColumn,
			SortDirection = sortColumnDirection?
			.Equals("desc", StringComparison.OrdinalIgnoreCase) == true ? SortDirection.Descending : SortDirection.Ascending
		};

		int CalculatePageNumber()
		{
			int recordsToSkip = start != null ? Convert.ToInt32(start) : 0;
			recordsToSkip = recordsToSkip >= 0 ? recordsToSkip : 0;
			return recordsToSkip / pageSize + 1;
		}
	}
}
