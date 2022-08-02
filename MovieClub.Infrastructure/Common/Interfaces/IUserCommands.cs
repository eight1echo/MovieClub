namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IUserCommands
{
    Task<int> CreateProfile(string userAccountId, string displayName);
    Task DeleteProfile(int userProfileId);
    Task UpdateProfile(int profileId, string name);
    Task RemoveUserProfile(string userAccountId);
}