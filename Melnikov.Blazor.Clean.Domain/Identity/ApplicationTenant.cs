using Melnikov.Blazor.Clean.Domain.Common;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public class ApplicationTenant : EntityBase<int>
{
    public string Name { get; set; }
    
    public string? Description { get; set; }
}