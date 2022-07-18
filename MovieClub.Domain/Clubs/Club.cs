namespace MovieClub.Domain;
public class Club : BaseEntity
{
    public Club(int userId, string clubName)
    {
        Name = clubName;
        _meetups = new List<Meetup>();
        _memberships = new List<Membership>() { new Membership(Id, userId, MembershipRank.Leader) };
    }

    public string Name { get; private set; }

    private readonly List<Membership> _memberships;
    public IReadOnlyCollection<Membership> Memberships => _memberships;

    private readonly List<Meetup> _meetups;
    public IReadOnlyCollection<Meetup> Meetups => _meetups;

    // Required for EF
    #nullable disable
    private Club() { }
}
