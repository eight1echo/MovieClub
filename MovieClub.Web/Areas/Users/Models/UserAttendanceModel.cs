namespace MovieClub.Web.Areas.Users.Models;

public class UserAttendanceModel
{
    public int UserProfileId { get; set;}
    public int AttendanceId { get; set; }

    public AttendanceStatus AttendanceStatus { get; set; }

    public AttendanceDTO? NextAttendance { get; set; } = new AttendanceDTO();
    public List<AttendanceDTO> FutureAttendance { get; set; } = new List<AttendanceDTO>();
    public List<AttendanceDTO> PastAttendance { get; set; } = new List<AttendanceDTO>();
}
