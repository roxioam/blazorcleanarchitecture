namespace Melnikov.Blazor.Clean.Application.Common.Models.Security;

public class PermissionModel
{
    public int? UserId { get; set; }
    
    public int? RoleId { get; set; }
    
    public string Description { get; set; } = "Permission Description";
    
    public string Group { get; set; } = "Permission";
    
    public required string ClaimType { get; set; }
    
    public required string ClaimValue { get; set; }
    
    public bool Assigned { get; set; }
}