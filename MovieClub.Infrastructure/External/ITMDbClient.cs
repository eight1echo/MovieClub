using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Infrastructure.External;

public interface ITMDbClient
{
    Task<TMDbMovie?> GetMovie(int id);
    Task<List<TMDbSearchResult>> SearchMovies(string searchString);
}