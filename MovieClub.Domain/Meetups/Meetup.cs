namespace MovieClub.Domain;
public class Meetup : BaseEntity
{
    public Meetup(int userId, int clubId, int movieId, DateTime date, bool hidden)
    {
        ClubId = clubId;
        MovieId = movieId;
        Date = date;
        MovieHidden = hidden;
        _attendance = new List<Attendance>() { new Attendance(Id, userId, AttendanceStatus.Hosting) };
    }

    public int ClubId { get; private set; }
    public Club? Club { get; set; }

    public int MovieId { get; private set; }
    public Movie? Movie { get; set; }

    public bool MovieHidden { get; private set; }

    public DateTime Date { get; private set; }


    private readonly List<Attendance> _attendance;
    public IReadOnlyCollection<Attendance> Attendance => _attendance;

    // Required for EF
    #nullable disable
    private Meetup() { }
}
