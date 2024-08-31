namespace Melnikov.Blazor.Clean.Infrastructure.Options;

public class SmtpClientOptions
{
    public const string SmtpClient = nameof(SmtpClient);

    public required string Server { get; set; }

    public required int Port { get; set; } = 587;
    
    public required string User { get; set; }
    
    public required string Password { get; set; }

    public bool UseSsl { get; set; } = true;

    public string? DefaultFromEmail { get; set; }
}