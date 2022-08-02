using MovieClub.Web.Areas.Meetups.Pages.Details;

namespace MovieClub.Web.Areas.Meetups;

public class MeetupService : IMeetupService
{
    private readonly ApplicationDbContext _context;

    public MeetupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MeetupDetailsModel> DetailsPage(int userProfileId, int meetupId)
    {
        var model = await _context.Meetups
            .Where(m => m.Id == meetupId)
            .Select(m => new MeetupDetailsModel
            {
                AttendanceAttending = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Attending)
                    .Select(a => new AttendanceDTO
                    {
                        UserProfile = new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName },
                        Status = a.Status
                    }).ToList(),

                AttendanceInvited = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Invited)
                    .Select(a => new AttendanceDTO
                    {
                        UserProfile = new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName },
                        Status = a.Status
                    }).ToList(),

                AttendanceDeclined = m.Attendance
                    .Where(a => a.Status == AttendanceStatus.Declined)
                    .Select(a => new AttendanceDTO
                    {
                        UserProfile = new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName },
                        Status = a.Status
                    }).ToList(),

                Club = new ClubDTO
                {
                    Id = m.Club.Id,
                    Name = m.Club.Name
                },

                Meetup = new MeetupDTO
                {
                    Id = m.Id,
                    Date = m.Date,

                    Host = m.Attendance
                            .Where(a => a.Status == AttendanceStatus.Hosting)
                            .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                            .First(),

                    Location = m.Location,

                    Movie = new MovieDTO
                    {
                        Genres = m.Movie.Genres,
                        PosterURL = m.Movie.PosterURL,
                        ReleaseDate = m.Movie.ReleaseDate,
                        Synopsis = m.Movie.Synopsis,
                        Tagline = m.Movie.Tagline,
                        Title = m.Movie.Title
                    }
                },

                UserAttendance = m.Attendance
                        .Where(a => a.UserProfileId == userProfileId)
                        .Select(a => new AttendanceDTO
                        {
                            Id = a.Id,
                            Status = a.Status
                        }).FirstOrDefault()

            }).FirstAsync();

        return model;
    }
}
