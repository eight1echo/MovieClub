namespace MovieClub.Web.Common.Interfaces;

public interface IUserQueryService
{
    Task<UserProfileDTO> GetCurrentUserProfile(ClaimsPrincipal user);
    Task<int> GetProfileIdFromSession(HttpContext httpContext, ClaimsPrincipal user);
}