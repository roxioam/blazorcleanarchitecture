namespace Melnikov.Blazor.Clean.Application.Common.Models.Filtering;

public class PaginationFilter : FilterBase
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}