using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public sealed class ApplicationUserClaim : IdentityUserClaim<int>, IEntity<int>
{
    public string? Description { get; set; }
    
    public ApplicationUser User { get; set; }
}