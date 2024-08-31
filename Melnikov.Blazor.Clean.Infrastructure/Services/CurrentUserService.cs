using Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;
using Melnikov.Blazor.Clean.Application.Common.Models.Security;
using Melnikov.Blazor.Clean.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Melnikov.Blazor.Clean.Infrastructure.Services;

public class CurrentUserService(AuthenticationStateProvider authenticationStateProvider) : ICurrentUserService
{
    public async Task<UserProfileModel?> GetCurrentUserProfile()
    {
        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        return authenticationState.User.GetUserProfileModel();
    }
}