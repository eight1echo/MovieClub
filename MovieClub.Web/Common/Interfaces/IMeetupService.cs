namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupService
{
    Task<MeetupDetailsModel?> GetMeetupDetails(int userProfileId, int meetupId);
}