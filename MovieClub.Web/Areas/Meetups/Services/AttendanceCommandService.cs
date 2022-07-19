namespace MovieClub.Web.Areas.Meetups.Services;

public class AttendanceCommandService : IAttendanceCommandService
{
    private readonly ApplicationDbContext _context;

    public AttendanceCommandService(ApplicationDbContext context)
    {
        _context = context;
    }
}
