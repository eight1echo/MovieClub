namespace MovieClub.Web.Areas.Movies.Services;

public class MovieQueryService : IMovieQueryService
{
    private readonly ApplicationDbContext _context;
    private readonly ITMDbClient _tmdb;

    public MovieQueryService(ApplicationDbContext context, ITMDbClient tmdb)
    {
        _context = context;
        _tmdb = tmdb;
    }

    public async Task<List<SelectListItem>> MovieSelectQuery(string searchValue)
    {
        var movies = await _tmdb.SearchMovies(searchValue);

        var movieSelect = movies.Select(m => new SelectListItem
        {
            Value = m.Id.ToString(),
            Text = $"{m.Title} ({m.Release_Date})"
        }).ToList() ?? new List<SelectListItem>();

        return movieSelect;
    }
}
