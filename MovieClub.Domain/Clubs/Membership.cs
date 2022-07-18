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
    public Club? Club { get; set; }

    public int UserProfileId { get; private set; }
    public UserProfile? UserProfile { get; set; }

    public MembershipRank Rank { get; set; }

    // Required for EF
    #nullable disable
    private Membership() { }
}
