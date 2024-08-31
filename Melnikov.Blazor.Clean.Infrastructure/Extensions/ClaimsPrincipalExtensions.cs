using System.Security.Claims;
using Melnikov.Blazor.Clean.Application.Common.Models.Security;
using Melnikov.Blazor.Clean.Infrastructure.Constants.Identity;

namespace Melnikov.Blazor.Clean.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static UserProfileModel? GetUserProfileModel(this ClaimsPrincipal claimsPrincipal)
    {
        if (!claimsPrincipal.Identity?.IsAuthenticated ?? true)
        {
            return null;
        }
        
        return new UserProfileModel
        {
            UserId = claimsPrincipal.GetUserId() ?? -1,
            UserName = claimsPrincipal.GetUserName() ?? string.Empty,
            FirstName = claimsPrincipal.GetFirstName() ?? string.Empty,
            MiddleName = claimsPrincipal.GetMiddleName(),
            LastName = claimsPrincipal.GetLastName() ?? string.Empty,
            Email = claimsPrincipal.GetEmail() ?? string.Empty,
            PhoneNumber = claimsPrincipal.GetPhoneNumber(),
            ProfilePictureDataUrl = claimsPrincipal.GetProfilePictureDataUrl(),
            TenantId = claimsPrincipal.GetTenantId(),
            TenantName = claimsPrincipal.GetTenantName(),
            SuperiorName = claimsPrincipal.GetSuperiorName(),
            SuperiorId = claimsPrincipal.GetSuperiorId(),
            AssignedRoles = claimsPrincipal.GetRoles(),
            IsActive = claimsPrincipal.GetStatus()
        };
    }

    public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
    }

    public static string? GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.MobilePhone);
    }

    public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var value = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        return string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
    }

    public static string? GetUserName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }

    public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
    }

    public static string? GetFirstName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.FirstName);
    }

    public static string? GetMiddleName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.MiddleName);
    }

    public static string? GetLastName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.LastName);
    }

    public static int? GetTenantId(this ClaimsPrincipal claimsPrincipal)
    {
        var value = claimsPrincipal.FindFirstValue(ApplicationClaimTypes.TenantId);
        return string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
    }

    public static string? GetTenantName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.TenantName);
    }

    public static int? GetSuperiorId(this ClaimsPrincipal claimsPrincipal)
    {
        var value = claimsPrincipal.FindFirstValue(ApplicationClaimTypes.SuperiorId);
        return string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
    }

    public static string? GetSuperiorName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.SuperiorName);
    }

    public static string? GetProfilePictureDataUrl(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ApplicationClaimTypes.ProfilePictureDataUrl);
    }

    public static bool GetStatus(this ClaimsPrincipal claimsPrincipal)
    {
        return Convert.ToBoolean(claimsPrincipal.FindFirstValue(ApplicationClaimTypes.Status));
    }
}