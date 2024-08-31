namespace Melnikov.Blazor.Clean.Web.UI.Constants;

public static class RouteConstants
{
    public const string HomePage = "/";
    public const string ErrorPage = "/error";
    public const string ProfilePage = "/profile";
    public const string SettingsPage = "/settings";
    
    public const string LoginPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.Login}";
    public const string AccessDeniedPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.AccessDenied}";
    public const string LockoutPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.Lockout}";
    public const string ForgotPasswordPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.ForgotPassword}";
    public const string ForgotPasswordConfirmationPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.ForgotPasswordConfirmation}";
    public const string ResetPasswordPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.ResetPassword}";
    public const string ResetPasswordConfirmationPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.ResetPasswordConfirmation}";
    public const string InvalidPasswordResetPage = $"{AccountRouteConstants.AccountGroupName}{AccountRouteConstants.InvalidPasswordReset}";
}