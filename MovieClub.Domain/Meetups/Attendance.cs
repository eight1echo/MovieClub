namespace MovieClub.Domain;
public class Attendance : BaseEntity
{
    public Attendance(int meetupId, int userId, AttendanceStatus status)
    {
        MeetupId = meetupId;
        UserProfileId = userId;
        Status = status;
    }
    public int MeetupId { get; private set; }
    public Meetup? Meetup { get; set; }

    public int UserProfileId { get; private set; }
    public UserProfile? UserProfile { get; set; }

    public AttendanceStatus Status { get; private set; }

    // Required for EF
    #nullable disable
    private Attendance() { }
}
