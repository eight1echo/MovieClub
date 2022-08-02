using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Infrastructure.Common.Interfaces;

public interface ITMDbClient
{
    Task<TMDbMovie?> GetMovie(int id);
    Task<List<TMDbSearchResult>> SearchMovies(string searchString);
}