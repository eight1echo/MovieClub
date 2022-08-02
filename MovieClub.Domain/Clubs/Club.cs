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
    public virtual IReadOnlyCollection<Membership> Memberships => _memberships;

    private readonly List<Meetup> _meetups;
    public virtual IReadOnlyCollection<Meetup> Meetups => _meetups;

    public void SetName(string name)
    {
        Name = name;
    }

    // Required for EF
    #nullable disable
    private Club() { }
}
