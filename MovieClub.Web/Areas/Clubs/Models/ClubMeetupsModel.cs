namespace MovieClub.Web.Areas.Clubs.Models;

public class ClubMeetupsModel
{
    public int UserProfileId { get; set; }

    public ClubDTO Club { get; set; } = new ClubDTO();
    public AttendanceDTO UserAttendance { get; set; } = new AttendanceDTO();
    public MeetupDTO? NextMeetup { get; set; }

    public List<MeetupDTO> FutureMeetups { get; set; } = new List<MeetupDTO>();
    public List<MeetupDTO> PastMeetups { get; set; } = new List<MeetupDTO>();
}
