namespace MovieClub.Web.Common.DTOs;
public class MeetupDTO
{
    public int Id { get; set; }
    public AttendanceDTO? UserAttendance { get; set; }

    public DateTime Date { get; set; }
    public string? Location { get; set; }

    public ClubDTO? Club { get; set; }
    public UserProfileDTO? Host { get; set; }
    public MovieDTO? Movie { get; set; }
}
