namespace MovieClub.Web.Areas.Clubs.Services;

public class MembershipQueryService : IMembershipQueryService
{
    private readonly ApplicationDbContext _context;

    public MembershipQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}
