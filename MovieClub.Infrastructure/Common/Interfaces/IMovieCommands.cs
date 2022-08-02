namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IMovieCommands
{
    Task<int> ImportMovie(int tmdbId);
}