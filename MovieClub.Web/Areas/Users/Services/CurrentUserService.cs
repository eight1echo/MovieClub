namespace MovieClub.Web.Areas.Users.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IUserQueryService _userQueries;
    private readonly UserManager<UserAccount> _userManager;

    public CurrentUserService(UserManager<UserAccount> userManager, IUserQueryService userQueries)
    {
        _userManager = userManager;
        _userQueries = userQueries;
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
            var userProfile = await _userQueries.GetUserProfile(userAccount.Id);

            if (userProfile is null)
                throw new Exception("User Profile does not exist.");

            httpContext.Session.SetInt32("UserProfileId", userProfile.Id);
            userProfileId = userProfile.Id;
        }

        return (int)userProfileId;
    }

    public async Task ClearSessionState(HttpContext httpContext)
    {
        httpContext.Session.Clear();
        await Task.CompletedTask;
    }
}
