namespace MovieClub.Web.Areas.Users.Services;

public class UserQueryService : IUserQueryService
{
    private readonly ApplicationDbContext _context;

    public UserQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDTO?> GetUserProfile(string userAccountId)
    {
        var profile = await _context.UserProfiles
            .Where(up => up.UserAccountId == userAccountId)
            .Select(up => new UserProfileDTO { Id = up.Id, DisplayName = up.DisplayName })
            .FirstOrDefaultAsync();

        return profile;
    }
}