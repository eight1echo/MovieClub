using MovieClub.Web.Areas.Meetups.Pages.Attendance;

namespace MovieClub.Web.Common.Interfaces;

public interface IAttendanceQueryService
{
    Task<EditAttendanceModel?> EditAttendancePage(int attendanceId);
    List<SelectListItem> StatusSelect();
}
