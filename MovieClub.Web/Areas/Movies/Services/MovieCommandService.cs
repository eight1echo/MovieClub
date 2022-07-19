namespace MovieClub.Web.Areas.Movies.Services;

public class MovieCommandService : IMovieCommandService
{
    private readonly ApplicationDbContext _context;

    public MovieCommandService(ApplicationDbContext context)
    {
        _context = context;
    }
}
