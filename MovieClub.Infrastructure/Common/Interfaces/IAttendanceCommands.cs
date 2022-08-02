namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IAttendanceCommands
{
    Task InviteClubMembers(int userProfileId, int clubId, int meetupId);
    Task UpdateStatus(int attendanceId, AttendanceStatus newStatus);
}