using System.ComponentModel.DataAnnotations.Schema;
using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public class ApplicationUser : IdentityUser<int>, IEntity<int>
{
    public ApplicationUser()
    {
        Roles = new List<ApplicationRole>();
        UserRoles = new List<ApplicationUserRole>();
        UserClaims = new HashSet<ApplicationUserClaim>();
        UserLogins = new HashSet<ApplicationUserLogin>();
        UserTokens = new HashSet<ApplicationUserToken>();
    }

    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string LastName { get; set; }

    public string? ProfilePictureDataUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public int? TenantId { get; set; }

    public ApplicationTenant? Tenant { get; set; }

    public int? SuperiorId { get; set; }

    public ApplicationUser? Superior { get; set; }

    public List<ApplicationRole> Roles { get; set; }

    public List<ApplicationUserRole> UserRoles { get; set; }

    public ICollection<ApplicationUserClaim> UserClaims { get; set; }

    public ICollection<ApplicationUserLogin> UserLogins { get; set; }

    public ICollection<ApplicationUserToken> UserTokens { get; set; }

    #region UI Properties

    [NotMapped]
    public List<int> RoleIds
    {
        get { return Roles.Select(r => r.Id).ToList(); }
        set { Roles = value.Select(id => new ApplicationRole { Id = id }).ToList(); }
    }
    
    [NotMapped]
    public string DisplayName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    #endregion
}