namespace MovieClub.Web.Areas.Clubs.Models;

public class ClubMembershipsModel
{
    public int ClubId { get; set; }
    public string? ClubName { get; set; }
    public int MembershipId { get; set; }

    public MembershipDTO UserMembership { get; set; } = new MembershipDTO();
    public MembershipDTO Leader { get; set; } = new MembershipDTO();
    public List<MembershipDTO> Memberships { get; set; } = new List<MembershipDTO>();
    public List<MembershipDTO> Pending { get; set; } = new List<MembershipDTO>();
}
