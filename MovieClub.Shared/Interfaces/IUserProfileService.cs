namespace MovieClub.Shared.Interfaces;

public interface IUserProfileService
{
    Task<int> CreateUserProfile(string userAccountId, string displayName);
    Task RemoveUserProfile(string userAccountId);
}