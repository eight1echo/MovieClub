using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupQueryService : IMeetupQueryService
{
    private readonly ApplicationDbContext _context;

    public MeetupQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MeetupHomeModel> MeetupHomeQuery(int userProfileId)
    {
        var meetups = await _context.Meetups
            .Where(m => m.Attendance.Any(a => a.UserProfileId == userProfileId) && m.Date > DateTime.Now)
            .Select(m => new MeetupDTO
            {
                Id = m.Id,
                Club = new ClubDTO { Name = m.Club.Name },
                Date = m.Date,
                Host = m.Attendance.First(a => a.Status == AttendanceStatus.Hosting).UserProfile.DisplayName,
                Movie = new MovieDTO { Title = m.Movie.Title }
            }).ToListAsync();

        return new MeetupHomeModel() { UpcomingMeetups = meetups };
    }
}
