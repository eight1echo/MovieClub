namespace MovieClub.Web.Areas.Clubs.Services;

public class MembershipCommandService : IMembershipCommandService
{
    private readonly ApplicationDbContext _context;

    public MembershipCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Accept(int clubId, int userId)
    {
        var membership = await _context.Memberships
            .Where(m => m.ClubId == clubId && m.UserProfileId == userId)
            .FirstOrDefaultAsync();

        if (membership is not null)
        {
            membership.AcceptMembership();

            var upcomingMeetups = await _context.Meetups
                .Include(m => m.Attendance)
                .Where(m => m.ClubId == clubId && m.Date > DateTime.Now)
                .ToListAsync();

            foreach (var meetup in upcomingMeetups)
            {
                meetup.InviteMember(userId, meetup.Id);
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task Cancel(int clubId, int userId)
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

    public async Task CreatePending(int clubId, int userId)
    {
        var membership = new Membership(clubId, userId, MembershipRank.Pending);

        await _context.Memberships.AddAsync(membership);
        await _context.SaveChangesAsync();
    }
}
