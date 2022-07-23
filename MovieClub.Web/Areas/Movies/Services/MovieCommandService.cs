using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Web.Areas.Movies.Services;

public class MovieCommandService : IMovieCommandService
{
    private readonly ApplicationDbContext _context;
    private readonly ITMDbClient _tmdb;

    public MovieCommandService(ApplicationDbContext context, ITMDbClient tmdb)
    {
        _context = context;
        _tmdb = tmdb;
    }

    public async Task<int> ImportMovie(int tmdbId)
    {
        var existingMovie = await _context.Movies.Where(m => m.TMDbId == tmdbId).FirstOrDefaultAsync();

        if (existingMovie == null)
        {
            var tmdbMovie = await _tmdb.GetMovie(tmdbId);

            if (tmdbMovie is not null)
            {
                var newMovie = new Movie(tmdbId)
                {
                    BackdropURL = tmdbMovie.Backdrop_Path,
                    Budget = tmdbMovie.Budget,
                    IMDbId = tmdbMovie.IMDb_Id,
                    Language = tmdbMovie.Original_Language,
                    LanguageTitle = tmdbMovie.Original_Title,
                    PosterURL = "https://image.tmdb.org/t/p/w500/" + tmdbMovie.Poster_Path,
                    ReleaseDate = tmdbMovie.Release_Date ?? DateTime.MinValue,
                    Revenue = tmdbMovie.Revenue,
                    Runtime = tmdbMovie.Runtime,
                    Synopsis = tmdbMovie.Overview,
                    Tagline = tmdbMovie.Tagline,
                    Title = tmdbMovie.Title
                };

                if (tmdbMovie.Genres is not null)
                {
                    var genres = tmdbMovie.Genres.Select(g => g.Name).ToArray();
                    newMovie.Genres = string.Join(", ", genres);
                }

                _context.Movies.Add(newMovie);
                await _context.SaveChangesAsync();

                return newMovie.Id;
            }

            throw new Exception($"Failed to find movie with TMDb Id: {tmdbId}");
        }

        return existingMovie.Id;
    }
}
