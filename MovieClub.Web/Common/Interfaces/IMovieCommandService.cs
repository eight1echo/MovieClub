namespace MovieClub.Web.Common.Interfaces;

public interface IMovieCommandService
{
    Task<int> ImportMovie(int tmdbId);
}
