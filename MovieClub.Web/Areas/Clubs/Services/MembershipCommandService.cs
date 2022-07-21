namespace MovieClub.Web.Areas.Clubs.Services;

public class MembershipCommandService : IMembershipCommandService
{
    private readonly ApplicationDbContext _context;

    public MembershipCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CancelMembership(int clubId, int userId)
    {
        var membership = await _context.Memberships
            .Where(m => m.ClubId == clubId && m.UserProfileId == userId)
            .FirstOrDefaultAsync();

        if (membership is not null)
        {
            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreatePendingMembership(int clubId, int userId)
    {
        var membership = new Membership(clubId, userId, MembershipRank.Pending);

        await _context.Memberships.AddAsync(membership);
        await _context.SaveChangesAsync();
    }
}
