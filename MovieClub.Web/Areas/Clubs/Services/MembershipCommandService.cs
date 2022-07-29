namespace MovieClub.Web.Areas.Clubs.Services;

public class MembershipCommandService : IMembershipCommandService
{
    private readonly ApplicationDbContext _context;

    public MembershipCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AcceptMembership(int clubId, int userId)
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

    public async Task DeleteMembership(int membershipId)
    {
        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == membershipId);

        if (membership is not null)
        {
            var attendance = await _context.Attendance
            .Where(a => a.UserProfileId == membership.UserProfileId && a.Meetup.ClubId == membership.ClubId)
            .ToListAsync();

            if (attendance.Any())
                _context.Attendance.RemoveRange(attendance);

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
