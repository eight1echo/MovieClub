namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IClubCommands
{
    Task<int> Create(int userProfileId, string name);
    Task DeleteClub(int clubId);
    Task UpdateClub(int clubId, string name);
}