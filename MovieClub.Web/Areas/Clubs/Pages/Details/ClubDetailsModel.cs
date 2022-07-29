namespace MovieClub.Web.Areas.Clubs.Pages.Details;

public class ClubDetailsModel
{
    public ClubDetailsModel()
    {
        UserMembership = new MembershipDTO() { Rank = MembershipRank.Visitor };
    }

    public int ClubId { get; set; }

    public MembershipDTO UserMembership { get; set; } = null!;

    public MembershipDTO ClubLeader { get; set; } = new MembershipDTO();
    public string? ClubName { get; set; }

    public List<MembershipDTO> ClubMembers { get; set; } = new List<MembershipDTO>();
    public List<MembershipDTO> PendingMemberships { get; set; } = new List<MembershipDTO>();

    public MeetupDTO NextMeetup { get; set; } = new MeetupDTO()!;
}
