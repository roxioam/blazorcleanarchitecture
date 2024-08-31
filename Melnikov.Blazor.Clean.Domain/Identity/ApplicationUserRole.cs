using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public sealed class ApplicationUserRole : IdentityUserRole<int>, IEntity
{
    public ApplicationUser User { get; set; }

    public ApplicationRole Role { get; set; }
}