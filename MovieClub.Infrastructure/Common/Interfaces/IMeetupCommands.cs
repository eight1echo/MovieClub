namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IMeetupCommands
{
    Task<int> CreateMeetup(int userId, int clubId, int movieId, DateTime date, string location);
}