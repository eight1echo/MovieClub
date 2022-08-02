namespace MovieClub.Web.Areas.Movies;

public class MovieService : IMovieService
{
    private readonly ITMDbClient _tmdb;

    public MovieService(ITMDbClient tmdb)
    {
        _tmdb = tmdb;
    }

    public async Task<List<SelectListItem>> GetSelectList(string searchValue)
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
