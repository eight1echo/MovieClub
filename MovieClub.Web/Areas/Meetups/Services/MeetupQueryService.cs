using MovieClub.Web.Areas.Clubs.Pages.Meetups;

namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupQueryService : IMeetupQueryService
{
    private readonly ApplicationDbContext _context;

    public MeetupQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClubMeetupsModel?> PastClubMeetups(int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                ClubId = c.Id,
                ClubName = c.Name,
                Meetups = c.Meetups
                    .Where(m => m.Date < DateTime.Now)
                    .OrderByDescending(m => m.Date)
                    .Select(m => new MeetupDTO
                    {
                        Id = m.Id,
                        Club = new ClubDTO { Id = m.Club.Id, Name = m.Club.Name },
                        Date = m.Date,
                        Host = m.Attendance.First(a => a.Status == AttendanceStatus.Hosting).UserProfile.DisplayName,
                        Location = m.Location,
                        Movie = new MovieDTO { Title = m.Movie.Title }
                    }).ToList()

            }).FirstOrDefaultAsync();

        return model;
    }

    public async Task<ClubMeetupsModel?> UpcomingClubMeetups(int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                ClubId = c.Id,
                ClubName = c.Name,
                Meetups = c.Meetups
                    .Where(m => m.Date > DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => new MeetupDTO
                    {
                        Id = m.Id,
                        Club = new ClubDTO { Id = m.Club.Id, Name = m.Club.Name },
                        Date = m.Date,
                        Host = m.Attendance.First(a => a.Status == AttendanceStatus.Hosting).UserProfile.DisplayName,
                        Location = m.Location,
                        Movie = new MovieDTO { Title = m.Movie.Title },
                        //UserAttendance = m.Attendance.First(a => a)
                    }).ToList()

            }).FirstOrDefaultAsync();

        return model;
    }
}
