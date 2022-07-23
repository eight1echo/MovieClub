namespace MovieClub.Web.Common.Interfaces;

public interface ICurrentUserService
{
    Task ClearSessionState(HttpContext httpContext);
    Task<UserAccount> GetCurrentUser(ClaimsPrincipal user);
    Task<string> GetDisplayNameFromSession(HttpContext httpContext, ClaimsPrincipal user);
    Task<int> GetProfileIdFromSession(HttpContext httpContext, ClaimsPrincipal user);
}