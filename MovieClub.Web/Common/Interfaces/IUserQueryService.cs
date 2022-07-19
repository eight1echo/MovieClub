namespace MovieClub.Web.Common.Interfaces;

public interface IUserQueryService
{
    Task<UserProfileDTO?> GetUserProfile(string userAccountId);
}