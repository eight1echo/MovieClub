namespace MovieClub.Web.Common.DTOs;
public class MembershipDTO
{
    public UserProfileDTO UserProfile { get; set; } = new UserProfileDTO();
    public MembershipRank Rank { get; set; }
}
