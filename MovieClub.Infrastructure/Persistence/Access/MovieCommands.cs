namespace MovieClub.Infrastructure.Persistence.Access;

public class MovieCommands : IMovieCommands
{
    private readonly ApplicationDbContext _context;
    private readonly ITMDbClient _tmdb;

    public MovieCommands(ApplicationDbContext context, ITMDbClient tmdb)
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
                    IMDbPath = tmdbMovie.IMDb_Id,
                    LetterboxPath = tmdbMovie.Title!.Replace(' ', '-').ToLower(),
                    PosterURL = "https://image.tmdb.org/t/p/w500/" + tmdbMovie.Poster_Path,
                    ReleaseDate = tmdbMovie.Release_Date ?? DateTime.MinValue,
                    Runtime = tmdbMovie.Runtime,
                    Synopsis = tmdbMovie.Overview,
                    Tagline = tmdbMovie.Tagline,
                    Title = tmdbMovie.Title
                };

                if (tmdbMovie.Credits is not null)
                {
                    if (tmdbMovie.Credits.Cast is not null)
                    {
                        var cast = tmdbMovie.Credits.Cast.Select(c => c.Name).Take(2).ToArray();
                        newMovie.Cast = string.Join(", ", cast);
                    }

                    if (tmdbMovie.Credits.Crew is not null)
                    {
                        var director = tmdbMovie.Credits.Crew.Where(c => c.Job == "Director").FirstOrDefault();
                        if (director is not null)
                        {
                            newMovie.Director = director.Name;
                        }

                        var screenwriter = tmdbMovie.Credits.Crew.Where(c => c.Job == "Screenplay").FirstOrDefault() ?? tmdbMovie.Credits.Crew.Where(c => c.Job == "Writer").FirstOrDefault();
                        if (screenwriter is not null)
                        {
                            newMovie.Screenwriter = screenwriter.Name;
                        }
                    }
                }

                if (tmdbMovie.Genres is not null)
                {
                    var genres = tmdbMovie.Genres.Select(g => g.Name).Take(3).ToArray();
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