namespace Melnikov.Blazor.Clean.Application.Common.Models.Security;

public class UserProfileModel
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ProfilePictureDataUrl { get; set; }

    public int? TenantId { get; set; }

    public string? TenantName { get; set; }

    public int? SuperiorId { get; set; }

    public string? SuperiorName { get; set; }

    public string[]? AssignedRoles { get; set; }

    public bool IsActive { get; set; }

    public string DisplayName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";
}