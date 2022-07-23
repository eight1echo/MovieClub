using MovieClub.Web.Areas.Clubs.Pages.Create;

namespace MovieClub.Web.Areas.Clubs.Services;

public class ClubCommandService : IClubCommandService
{
    private readonly ApplicationDbContext _context;

    public ClubCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(int userProfileId, CreateClubModel model)
    {
        var newClub = new Club(userProfileId, model.Name!);

        _context.Clubs.Add(newClub);
        await _context.SaveChangesAsync();

        return newClub.Id;
    }

    public async Task Delete(int clubId)
    {
        var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);

        if (club != null)
        {
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
        }
    }
}
