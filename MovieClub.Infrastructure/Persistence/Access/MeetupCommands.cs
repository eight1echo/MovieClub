namespace MovieClub.Infrastructure.Persistence.Access;

public class MeetupCommands : IMeetupCommands
{
    private readonly ApplicationDbContext _context;

    public MeetupCommands(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateMeetup(int userId, int clubId, int movieId, DateTime date, string location)
    {
        var meetup = new Meetup(userId, clubId, movieId, date, location);

        _context.Meetups.Add(meetup);
        await _context.SaveChangesAsync();

        return meetup.Id;
    }
}
