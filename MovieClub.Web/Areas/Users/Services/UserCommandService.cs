namespace MovieClub.Web.Areas.Users.Services;

public class UserCommandService : IUserCommandService
{
    private readonly ApplicationDbContext _context;

    public UserCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUserProfile(string userAccountId, string displayName)
    {
        var newProfile = new UserProfile(userAccountId, displayName);

        _context.UserProfiles.Add(newProfile);
        await _context.SaveChangesAsync();

        return newProfile.Id;
    }

    public async Task RemoveUserProfile(string userAccountId)
    {
        var profile = _context.UserProfiles.FirstOrDefault(up => up.UserAccountId == userAccountId);

        if (profile != null)
        {
            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}