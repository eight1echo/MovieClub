namespace MovieClub.Web.Areas.Movies.Services;

public class MovieQueryService : IMovieQueryService
{
    private readonly ApplicationDbContext _context;

    public MovieQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}
