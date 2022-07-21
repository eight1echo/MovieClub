using MovieClub.Web.Areas.Meetups.Pages.Create;

namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupCommandService : IMeetupCommandService
{
    private readonly ApplicationDbContext _context;

    public MeetupCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateMeetup(CreateMeetupModel model)
    {
        var meetup = new Meetup(model.UserProfileId, model.ClubId, model.MovieId, model.Date, model.MovieHidden);

        _context.Meetups.Add(meetup);
        await _context.SaveChangesAsync();
    }
}

