namespace MovieClub.Domain;
public class Membership : BaseEntity
{
    public Membership(int clubId, int userId, MembershipRank rank)
    {
        ClubId = clubId;
        UserProfileId = userId;
        Rank = rank;
    }

    public int ClubId { get; private set; }
    public virtual Club Club { get; set; } = null!;

    public int UserProfileId { get; private set; }
    public virtual UserProfile UserProfile { get; set; } = null!;

    public MembershipRank Rank { get; set; }

    // Required for EF
    private Membership() { }
}
