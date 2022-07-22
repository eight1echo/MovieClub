namespace MovieClub.Web.Common.Interfaces;

public interface IAttendanceCommandService
{
    Task InviteClubMembers(int userProfileId, int clubId, int meetupId);
}
