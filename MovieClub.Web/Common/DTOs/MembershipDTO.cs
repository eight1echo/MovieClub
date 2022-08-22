namespace MovieClub.Web.Common.DTOs;
public class MembershipDTO
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public ClubDTO Club { get; set; } = new ClubDTO();
    public UserProfileDTO UserProfile { get; set; } = new UserProfileDTO();
    public MembershipRank Rank { get; set; } = MembershipRank.Visitor;
}
