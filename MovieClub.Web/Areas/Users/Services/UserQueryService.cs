namespace MovieClub.Web.Areas.Users.Services;

public class UserQueryService : IUserQueryService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserAccount> _userManager;

    public UserQueryService(ApplicationDbContext context, UserManager<UserAccount> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> GetProfileIdFromSession(HttpContext httpContext, ClaimsPrincipal user)
    {
        int? userProfileId = httpContext.Session.GetInt32("UserProfileId");

        if (userProfileId is null)
        {
            var userProfile = await GetCurrentUserProfile(user);
            userProfileId = userProfile.Id;
        }

        return (int)userProfileId;
    }

    public async Task<UserProfileDTO> GetCurrentUserProfile(ClaimsPrincipal user)
    {
        var currentUser = _userManager.GetUserId(user);

        var profile = await _context.UserProfiles
            .Where(up => up.UserAccountId == currentUser)
            .Select(up => new UserProfileDTO { Id = up.Id })
            .FirstOrDefaultAsync();

        return profile;
    }
}