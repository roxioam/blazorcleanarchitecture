using System.Security.Claims;
using Melnikov.Blazor.Clean.Domain.Identity;
using Melnikov.Blazor.Clean.Infrastructure.Constants.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Melnikov.Blazor.Clean.Infrastructure.Services;

public class ApplicationUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);

        if (principal.Identity is not ClaimsIdentity claimsIdentity)
        {
            return principal;
        }
        
        if (!string.IsNullOrEmpty(user.FirstName))
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.FirstName, user.FirstName) });
        }

        if (!string.IsNullOrEmpty(user.MiddleName))
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.MiddleName, user.MiddleName) });
        }

        if (!string.IsNullOrEmpty(user.LastName))
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.LastName, user.LastName) });
        }

        if (user.TenantId.HasValue)
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.TenantId, user.TenantId.ToString()!) });
        }

        if (user.Tenant is not null && !string.IsNullOrWhiteSpace(user.Tenant.Name))
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.TenantName, user.Tenant.Name) });
        }

        if (user.SuperiorId.HasValue)
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.SuperiorId, user.SuperiorId.ToString()!) });
        }
        
        if (user.Superior != null && !string.IsNullOrWhiteSpace(user.Superior.DisplayName))
        {
            claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.SuperiorName, user.Superior.DisplayName) });
        }

        if (!string.IsNullOrEmpty(user.ProfilePictureDataUrl))
        {
            claimsIdentity.AddClaims(new[]
                { new Claim(ApplicationClaimTypes.ProfilePictureDataUrl, user.ProfilePictureDataUrl) });
        }

        claimsIdentity.AddClaims(new[] { new Claim(ApplicationClaimTypes.Status, user.IsActive.ToString()) });

        return principal;
    }
}