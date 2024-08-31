using Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;
using Melnikov.Blazor.Clean.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Melnikov.Blazor.Clean.Web.UI.Services;

internal sealed class IdentityEmailSender(IEmailService emailService) : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        emailService.SendEmailAsync(email, "Confirm your email",
            $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        emailService.SendEmailAsync(email, "Reset your password",
            $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        emailService.SendEmailAsync(email, "Reset your password",
            $"Please reset your password using the following code: {resetCode}");
}