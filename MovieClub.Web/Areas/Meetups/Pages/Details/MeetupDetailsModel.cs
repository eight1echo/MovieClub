namespace MovieClub.Web.Areas.Meetups.Pages.Details;

public class MeetupDetailsModel
{
    public MeetupDetailsModel()
    {
    }

    public ClubDTO Club { get; set; }

    public AttendanceDTO? UserAttendance { get; set; }

    public MeetupDTO Meetup { get; set; } = new MeetupDTO()!;

    public List<AttendanceDTO> AttendanceAttending { get; set; }
    public List<AttendanceDTO> AttendanceInvited { get; set; }
    public List<AttendanceDTO> AttendanceDeclined { get; set; }
}