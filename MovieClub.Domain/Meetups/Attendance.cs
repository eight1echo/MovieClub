namespace MovieClub.Domain;
public class Attendance : BaseEntity
{
    public Attendance(int userId, int meetupId, AttendanceStatus status)
    {
        MeetupId = meetupId;
        UserProfileId = userId;
        Status = status;
    }
    public int MeetupId { get; private set; }
    public virtual Meetup Meetup { get; set; } = null!;

    public int UserProfileId { get; private set; }
    public virtual UserProfile UserProfile { get; set; } = null!;

    public AttendanceStatus Status { get; private set; }

    public void SetStatus(AttendanceStatus newStatus)
    {
        Status = newStatus;
    }

    // Required for EF
    private Attendance() { }
}
