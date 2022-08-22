using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MovieClub.Infrastructure.Common.Interfaces;

public interface ICurrentUserService
{
    Task ClearSessionState(HttpContext httpContext);
    Task<UserAccount> GetCurrentUser(ClaimsPrincipal user);
    Task<int> GetUserProfileId(HttpContext httpContext, ClaimsPrincipal user);
}