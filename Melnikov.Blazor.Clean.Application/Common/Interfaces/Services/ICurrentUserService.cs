using Melnikov.Blazor.Clean.Application.Common.Models.Security;

namespace Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    Task<UserProfileModel?> GetCurrentUserProfile();
}