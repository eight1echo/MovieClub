namespace MovieClub.Web.Areas.Meetups;

public class MeetupService : IMeetupService
{
    private readonly ApplicationDbContext _context;

    public MeetupService(ApplicationDbContext context)
    {
        _context = context;
    }
}
