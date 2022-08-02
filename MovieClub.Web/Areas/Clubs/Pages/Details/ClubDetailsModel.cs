namespace MovieClub.Web.Areas.Clubs.Pages.Details;

public class ClubDetailsModel
{
    public ClubDetailsModel()
    {
        UserMembership = new MembershipDTO() { Rank = MembershipRank.Visitor };
    }

    public ClubDTO Club { get; set; }

    public MembershipDTO UserMembership { get; set; }

    public List<MembershipDTO> ClubMembers { get; set; } = new List<MembershipDTO>();
    public List<MembershipDTO> PendingMemberships { get; set; } = new List<MembershipDTO>();

    public MeetupDTO? NextMeetup { get; set; }
}
