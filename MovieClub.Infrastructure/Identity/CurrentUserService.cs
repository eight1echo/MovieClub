using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MovieClub.Infrastructure.Common.Interfaces;

namespace MovieClub.Infrastructure.Identity;
public class CurrentUserService : ICurrentUserService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserAccount> _userManager;

    public CurrentUserService(UserManager<UserAccount> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<UserAccount> GetCurrentUser(ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);

        return currentUser;
    }

    public async Task<int> GetProfileIdFromSession(HttpContext httpContext, ClaimsPrincipal user)
    {
        int? userProfileId = httpContext.Session.GetInt32("UserProfileId");

        if (userProfileId is null)
        {
            var userAccount = await GetCurrentUser(user);
            var userProfile = await _context.UserProfiles
                .Where(up => up.UserAccountId == userAccount.Id)
                .FirstOrDefaultAsync();

            if (userProfile is null)
                return 0;

            httpContext.Session.SetInt32("UserProfileId", userProfile.Id);
            userProfileId = userProfile.Id;
        }

        return (int)userProfileId;
    }

    public async Task<string> GetDisplayNameFromSession(HttpContext httpContext, ClaimsPrincipal user)
    {
        string? userDisplayName = httpContext.Session.GetString("UserDisplayName");

        if (userDisplayName is null)
        {
            var userAccount = await GetCurrentUser(user);
            var userProfile = await _context.UserProfiles
                .Where(up => up.UserAccountId == userAccount.Id)
                .FirstOrDefaultAsync();

            if (userProfile is null || userProfile.DisplayName is null)
                throw new Exception("User Profile does not exist.");

            httpContext.Session.SetString("UserDisplayName", userProfile.DisplayName);
            userDisplayName = userProfile.DisplayName;
        }

        return userDisplayName;
    }

    public async Task ClearSessionState(HttpContext httpContext)
    {
        httpContext.Session.Clear();
        await Task.CompletedTask;
    }
}
