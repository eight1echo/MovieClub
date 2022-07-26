namespace MovieClub.Web.Areas.Meetups.Services;

public class AttendanceCommandService : IAttendanceCommandService
{
    private readonly ApplicationDbContext _context;

    public AttendanceCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InviteClubMembers(int userProfileId, int clubId, int meetupId)
    {
        // Creates Pending Attendance for all Club Members except for the current user.

        var clubMembers = await _context.Memberships
            .Where(m => m.ClubId == clubId && m.Rank != MembershipRank.Pending && m.UserProfileId != userProfileId)
            .ToListAsync();

        var attendance = new List<Attendance>();
        foreach (var member in clubMembers)
        {
            attendance.Add(new Attendance(userProfileId, meetupId, AttendanceStatus.Invited));
        }

        _context.AddRange(attendance);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int attendanceId, AttendanceStatus newStatus)
    {
        var attendance = await _context.Attendance.FirstOrDefaultAsync(a => a.Id == attendanceId);

        if (attendance is not null)
        {
            attendance.SetStatus(newStatus);
            await _context.SaveChangesAsync();
        }
    }
}
