namespace MovieClub.Web.Common.Interfaces;

public interface IUserService
{
    Task<UserMembershipsModel?> GetUserMemberships(int userProfileId);
    Task<UserAttendanceModel?> GetUserFutureAttendance(int userProfileId);
    Task<UserAttendanceModel?> GetUserPastAttendance(int userProfileId);
}