using MovieClub.Web.Areas.Meetups.Pages.Create;

namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupCommandService : IMeetupCommandService
{
    private readonly ApplicationDbContext _context;

    public MeetupCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateMeetup(CreateMeetupModel model, int movieId)
    {
        var meetup = new Meetup(model.UserProfileId, model.ClubId, movieId, model.Date, model.Location);

        _context.Meetups.Add(meetup);
        await _context.SaveChangesAsync();

        return meetup.Id;
    }
}

