namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupCommandService : IMeetupCommandService
{
    private readonly ApplicationDbContext _context;

    public MeetupCommandService(ApplicationDbContext context)
    {
        _context = context;
    }
}
