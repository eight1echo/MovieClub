namespace MovieClub.Web.Areas.Clubs.Pages.Details;

public class ClubDetailsModel
{
    public MembershipRank? UserRank { get; set; }
    public string? ClubLeader { get; set; }
    public string? ClubName { get; set; }
    public List<MembershipDTO> ClubMembers { get; set; } = new List<MembershipDTO>();
    public List<MeetupDTO> UpcomingMeetups { get; set; } = new List<MeetupDTO>();
}
