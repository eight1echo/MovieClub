namespace MovieClub.Web.Areas.Home;

public class ClubHomeModel
{
    public List<ClubDTO> ClubsLeader { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsMember { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsPending { get; set; } = new List<ClubDTO>();

    public List<MeetupDTO> UpcomingMeetups { get; set; } = new List<MeetupDTO>();
}
