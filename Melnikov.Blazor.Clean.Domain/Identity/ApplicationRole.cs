using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Domain.Identity;

public class ApplicationRole : IdentityRole<int>, IEntity<int>
{
    public ApplicationRole()
    {
        Users = new List<ApplicationUser>();
        UserRoles = new List<ApplicationUserRole>();
        RoleClaims = new HashSet<ApplicationRoleClaim>();
    }
    
    public int? TenantId { get; set; }
    
    public ApplicationTenant? Tenant { get; set; }
    
    public string? Description { get; set; }
    
    public List<ApplicationUser> Users { get; set; }
    
    public List<ApplicationUserRole> UserRoles { get; set; }
    
    public ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
}