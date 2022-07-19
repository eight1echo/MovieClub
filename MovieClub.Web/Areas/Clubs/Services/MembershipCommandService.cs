namespace MovieClub.Web.Areas.Clubs.Services;

public class MembershipCommandService : IMembershipCommandService
{
    private readonly ApplicationDbContext _context;

    public MembershipCommandService(ApplicationDbContext context)
    {
        _context = context;
    }
}
