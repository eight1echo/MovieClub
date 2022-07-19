namespace MovieClub.Web.Common.Interfaces;

public interface IUserCommandService
{
    Task<int> CreateUserProfile(string userAccountId, string displayName);
    Task RemoveUserProfile(string userAccountId);
}