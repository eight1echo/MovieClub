namespace MovieClub.Infrastructure.Persistence.Access;
public class ClubCommands : IClubCommands
{
    private readonly ApplicationDbContext _context;

    public ClubCommands(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(int userProfileId, string name)
    {
        var newClub = new Club(userProfileId, name);

        _context.Clubs.Add(newClub);
        await _context.SaveChangesAsync();

        return newClub.Id;
    }

    public async Task DeleteClub(int clubId)
    {
        var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);

        if (club != null)
        {
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateClub(int clubId, string name)
    {
        var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);

        if (club != null)
        {
            club.SetName(name);

            _context.Update(club);
            await _context.SaveChangesAsync();
        }
    }
}