namespace MovieClub.Web.Areas.Users.Pages.Meetups;

public class UserMeetupsModel
{
    public UserProfileDTO UserProfile { get; set; } = new UserProfileDTO();

    public List<AttendanceDTO> Attendance { get; set; } = new List<AttendanceDTO>();
}
