using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public sealed class ApplicationRoleClaim : IdentityRoleClaim<int>, IEntity<int>
{
    public string? Description { get; set; }

    public string? Group { get; set; }

    public ApplicationRole Role { get; set; }
}