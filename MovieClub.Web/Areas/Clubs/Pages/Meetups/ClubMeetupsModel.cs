namespace MovieClub.Web.Areas.Clubs.Pages.Meetups;

public class ClubMeetupsModel
{
    public ClubDTO Club { get; set; } = new ClubDTO();

    public List<AttendanceDTO> Attendance { get; set; } = new List<AttendanceDTO>();
}
