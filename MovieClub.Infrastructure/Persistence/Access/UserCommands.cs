using MovieClub.Infrastructure.Common.Interfaces;

namespace MovieClub.Infrastructure.Persistence.Access;

public class UserCommands : IUserCommands
{
    private readonly ApplicationDbContext _context;

    public UserCommands(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateProfile(string userAccountId, string displayName)
    {
        var newProfile = new UserProfile(userAccountId, displayName);

        _context.UserProfiles.Add(newProfile);
        await _context.SaveChangesAsync();

        return newProfile.Id;
    }

    public async Task DeleteProfile(int userProfileId)
    {
        var profile = _context.UserProfiles
            .Include(up => up.Memberships)
                .ThenInclude(m => m.Club)
            .FirstOrDefault(up => up.Id == userProfileId);

        if (profile != null)
        {
            foreach (var membership in profile.Memberships)
            {
                _context.Clubs.Remove(membership.Club);
            }

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateProfile(int profileId, string name)
    {
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.Id == profileId);

        if (userProfile != null)
        {
            userProfile.SetName(name);

            _context.Update(userProfile);
            await _context.SaveChangesAsync();
        }
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