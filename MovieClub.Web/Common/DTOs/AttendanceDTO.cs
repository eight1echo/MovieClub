namespace MovieClub.Web.Common.DTOs;
public class AttendanceDTO
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public MeetupDTO? Meetup { get; set; }
    public UserProfileDTO? UserProfile { get; set; }
    public AttendanceStatus Status { get; set; }
}
