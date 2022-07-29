namespace MovieClub.Web.Areas.Users.Pages.Home;

public class HomePageModel
{
    public UserProfileDTO? UserProfile { get; set; }
    public List<ClubDTO> ClubsLeader { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsMember { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsPending { get; set; } = new List<ClubDTO>();

    public List<MeetupDTO> UpcomingMeetups { get; set; } = new List<MeetupDTO>();
}