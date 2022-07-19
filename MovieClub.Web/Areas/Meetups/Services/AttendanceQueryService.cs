namespace MovieClub.Web.Areas.Meetups.Services;

public class AttendanceQueryService : IAttendanceQueryService
{
    private readonly ApplicationDbContext _context;

    public AttendanceQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}
