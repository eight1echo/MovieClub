namespace MovieClub.Web.Areas.Meetups.Services;

public class AttendanceQueryService : IAttendanceQueryService
{
    private readonly ApplicationDbContext _context;

    public AttendanceQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<SelectListItem> StatusSelect()
    {
        var select = new List<SelectListItem> 
        {
            new SelectListItem { Text = AttendanceStatus.Attending.ToString(), Value = ((int)AttendanceStatus.Attending).ToString() },
            new SelectListItem { Text = "Undecided", Value = ((int)AttendanceStatus.Invited).ToString() },
            new SelectListItem { Text = "Can't Attend", Value = ((int)AttendanceStatus.Declined).ToString() },
        };

        return select;
    }
}
