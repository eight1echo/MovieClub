namespace MovieClub.Web.Areas.Users.Pages.Profile;

public class UserProfileModel
{
    public UserProfileDTO UserProfile { get; set; }

    public List<MembershipDTO> Leaderships { get; set; }
    public List<MembershipDTO> Memberships { get; set; }
    public List<MembershipDTO> Pending { get; set; }

    public AttendanceDTO? NextAttendance { get; set; }
}