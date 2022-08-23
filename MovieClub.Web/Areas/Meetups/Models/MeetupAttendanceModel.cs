namespace MovieClub.Web.Areas.Meetups.Models;

public class MeetupAttendanceModel
{
    public List<AttendanceDTO> Attending { get; set; } = new List<AttendanceDTO>();
    public List<AttendanceDTO> Declined { get; set; } = new List<AttendanceDTO>();
    public List<AttendanceDTO> Invited { get; set; } = new List<AttendanceDTO>();
}
