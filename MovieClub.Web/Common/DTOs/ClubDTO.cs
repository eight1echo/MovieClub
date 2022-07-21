namespace MovieClub.Web.Common.DTOs;
public class ClubDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? ClubLeader
    {
        get
        {
            return Memberships.Where(m => m.Rank == MembershipRank.Leader).First().UserProfile.DisplayName;
        }
    }

    public MembershipRank UserRank { get; set; }

    public ICollection<MeetupDTO> Meetups { get; set; } = new List<MeetupDTO>();
    public ICollection<MembershipDTO> Memberships { get; set; } = new List<MembershipDTO>();
}
