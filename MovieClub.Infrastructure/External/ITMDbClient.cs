using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Infrastructure.External;

public interface ITMDbClient
{
    Task<List<TMDbSearchResult>> SearchMovies(string searchString);
}