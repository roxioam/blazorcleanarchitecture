using System.Net;
using System.Net.Mail;
using Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;
using Melnikov.Blazor.Clean.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Melnikov.Blazor.Clean.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClientOptions _smtpClientOptions;
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<SmtpClientOptions> smtpClientOptions)
    {
        _smtpClientOptions = smtpClientOptions.Value;

        _smtpClient = new SmtpClient(_smtpClientOptions.Server)
        {
            Port = _smtpClientOptions.Port,
            Credentials = new NetworkCredential(_smtpClientOptions.User, _smtpClientOptions.Password),
            EnableSsl = _smtpClientOptions.UseSsl
        };
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };

        if (!string.IsNullOrWhiteSpace(_smtpClientOptions.DefaultFromEmail))
        {
            mailMessage.From = new MailAddress(_smtpClientOptions.DefaultFromEmail);
        }

        mailMessage.To.Add(email);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}