namespace MovieClub.Web.Areas.Users.Models;

public class UserMembershipsModel
{
    public int UserProfileId { get; set; }
    public string? UserDisplayName { get; set; }
    public int MembershipId { get; set; }

    public List<MembershipDTO> Leaderships { get; set; } = new List<MembershipDTO>();
    public List<MembershipDTO> Memberships { get; set; } = new List<MembershipDTO>();
    public List<MembershipDTO> Pending { get; set; } = new List<MembershipDTO>();
}
