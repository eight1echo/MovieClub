using MovieClub.Web.Areas.Meetups.Pages.Attendance;

namespace MovieClub.Web.Areas.Meetups.Services;

public class AttendanceQueryService : IAttendanceQueryService
{
    private readonly ApplicationDbContext _context;

    public AttendanceQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<SelectListItem> StatusSelect()
    {
        var select = new List<SelectListItem> 
        {
            new SelectListItem { Text = AttendanceStatus.Attending.ToString(), Value = ((int)AttendanceStatus.Attending).ToString() },
            new SelectListItem { Text = AttendanceStatus.Invited.ToString(), Value = ((int)AttendanceStatus.Invited).ToString() },
            new SelectListItem { Text = "Can't Attend", Value = ((int)AttendanceStatus.Declined).ToString() },
        };

        return select;
    }

    public async Task<EditAttendanceModel?> EditAttendancePage(int attendanceId)
    {
        var attendance = await _context.Attendance
            .Where(a => a.Id == attendanceId)
            .Select(a => new EditAttendanceModel
            {
                AttendanceId = a.Id,
                ClubName = a.Meetup.Club.Name,
                Status = a.Status,

                Meetup = new MeetupDTO
                {
                    Id = a.Meetup.Id,
                    Club = new ClubDTO
                    {
                        Id = a.Meetup.Club.Id,
                        Name = a.Meetup.Club.Name,
                    },
                    Date = a.Meetup.Date,
                    Host = a.Meetup.Attendance.Where(a => a.Status == AttendanceStatus.Hosting).First().UserProfile.DisplayName,
                    Location = a.Meetup.Location,
                    Movie = new MovieDTO
                    {
                        Genres = a.Meetup.Movie.Genres,
                        PosterURL = a.Meetup.Movie.PosterURL,
                        ReleaseDate = a.Meetup.Movie.ReleaseDate,
                        Synopsis = a.Meetup.Movie.Synopsis,
                        Tagline = a.Meetup.Movie.Tagline,
                        Title = a.Meetup.Movie.Title,
                    }
                }
                
            }).FirstOrDefaultAsync();

        return attendance;
    }
}
