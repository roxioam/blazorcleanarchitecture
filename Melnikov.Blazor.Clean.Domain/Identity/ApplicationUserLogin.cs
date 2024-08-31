using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public sealed class ApplicationUserLogin : IdentityUserLogin<int>, IEntity
{
    public ApplicationUser User { get; set; }
}