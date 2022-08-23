namespace MovieClub.Web.Areas.Meetups;

public class MeetupService : IMeetupService
{
    private readonly ApplicationDbContext _context;

    public MeetupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MeetupDetailsModel?> GetMeetupDetails(int userProfileId, int meetupId)
    {
        var model = await _context.Meetups
            .Where(m => m.Id == meetupId)
            .Select(m => new MeetupDetailsModel
            {
                Meetup = new MeetupDTO
                {
                    Id = m.Id,

                    UserAttendance = m.Attendance
                        .Where(a => a.UserProfileId == userProfileId)
                        .Select(a => new AttendanceDTO
                        {
                            Id = a.Id,
                            Status = a.Status
                        }).FirstOrDefault(),

                    Date = m.Date,
                    Location = m.Location,

                    Host = m.Attendance
                        .Where(a => a.Status == AttendanceStatus.Hosting)
                        .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                        .FirstOrDefault(),

                    Movie = new MovieDTO
                    {
                        Cast = m.Movie.Cast,
                        Director = m.Movie.Director,
                        Genres = m.Movie.Genres,
                        PosterURL = m.Movie.PosterURL,
                        ReleaseDate = m.Movie.ReleaseDate,
                        Runtime = m.Movie.Runtime,
                        Screenwriter = m.Movie.Screenwriter,
                        Synopsis = m.Movie.Synopsis,
                        Tagline = m.Movie.Tagline,
                        Title = m.Movie.Title
                    },
                },

                Attendance = new MeetupAttendanceModel
                {
                    Attending = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Attending)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,
                        UserProfile = new UserProfileDTO
                        {
                            DisplayName = a.UserProfile.DisplayName
                        }
                    }).ToList(),

                    Declined = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Declined)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,
                        UserProfile = new UserProfileDTO
                        {
                            DisplayName = a.UserProfile.DisplayName
                        }
                    }).ToList(),

                    Invited = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Invited)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,
                        UserProfile = new UserProfileDTO
                        {
                            DisplayName = a.UserProfile.DisplayName
                        }
                    }).ToList(),
                }

            }).FirstOrDefaultAsync();

        return model;
    }
}
