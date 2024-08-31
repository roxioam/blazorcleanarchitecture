namespace Melnikov.Blazor.Clean.Application.Common.Interfaces.Services;

public interface INotificationService
{
    void ShowError(string message);
    
    void ShowSuccess(string message);
}