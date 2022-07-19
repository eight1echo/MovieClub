namespace MovieClub.Web.Areas.Meetups.Services;

public class MeetupQueryService : IMeetupQueryService
{
    private readonly ApplicationDbContext _context;

    public MeetupQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}
