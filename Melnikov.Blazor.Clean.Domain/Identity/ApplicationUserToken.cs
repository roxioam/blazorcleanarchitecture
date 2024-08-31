using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public sealed class ApplicationUserToken : IdentityUserToken<int>, IEntity
{
    public ApplicationUser User { get; set; }
}