using MovieClub.Web.Areas.Meetups.Pages.Create;

namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupCommandService : IMeetupCommandService
{
    private readonly ApplicationDbContext _context;
    private readonly IAttendanceCommandService _attendanceCommands;
    private readonly IMovieCommandService _movieCommands;

    public MeetupCommandService(ApplicationDbContext context, IAttendanceCommandService attendanceCommands, IMovieCommandService movieCommands)
    {
        _context = context;
        _attendanceCommands = attendanceCommands;
        _movieCommands = movieCommands;
    }

    public async Task<int> CreateMeetup(CreateMeetupModel model, int movieId)
    {
        var meetup = new Meetup(model.UserProfileId, model.ClubId, movieId, model.Date, model.MovieHidden);

        _context.Meetups.Add(meetup);
        await _context.SaveChangesAsync();

        return meetup.Id;
    }
}

