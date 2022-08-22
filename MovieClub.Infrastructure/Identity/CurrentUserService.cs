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

    public async Task<int> GetUserProfileId(HttpContext httpContext, ClaimsPrincipal user)
    {
        int? userProfileId = httpContext.Session.GetInt32("UserProfileId");

        if (userProfileId is not null)
            return (int)userProfileId;

        var userAccount = await _userManager.GetUserAsync(user);
        var userProfile = await _context.UserProfiles
            .Where(up => up.UserAccountId == userAccount.Id)
            .FirstOrDefaultAsync();

        if (userProfile is not null)
        {
            httpContext.Session.SetInt32("UserProfileId", userProfile.Id);
            return userProfile.Id;
        }
        
        // Profile not found, return 0 to redirect to CreateProfilePage
        return 0;
    }

    public async Task ClearSessionState(HttpContext httpContext)
    {
        httpContext.Session.Clear();
        await Task.CompletedTask;
    }
}
