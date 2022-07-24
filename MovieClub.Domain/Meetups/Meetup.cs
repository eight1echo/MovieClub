namespace MovieClub.Domain;
public class Meetup : BaseEntity
{
    public Meetup(int userId, int clubId, int movieId, DateTime date, string location)
    {
        ClubId = clubId;
        MovieId = movieId;
        Date = date;
        Location = location;
        _attendance = new List<Attendance>() { new Attendance(userId, Id, AttendanceStatus.Hosting) };
    }

    public int ClubId { get; private set; }
    public virtual Club Club { get; set; } = null!;

    public int MovieId { get; private set; }
    public virtual Movie Movie { get; set; } = null!;

    public bool MovieHidden { get; private set; }

    public DateTime Date { get; private set; }

    public string? Location { get; set; }


    private readonly List<Attendance> _attendance;
    public virtual IReadOnlyCollection<Attendance> Attendance => _attendance;

    public void InviteMember(int userId, int meetupId)
    {
        _attendance.Add(new Attendance(userId, meetupId, AttendanceStatus.Invited));
    }


    // Required for EF
    #nullable disable
    private Meetup() { }
}
