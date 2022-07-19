using MovieClub.Web.Areas.Clubs.Pages.Index;

namespace MovieClub.Web.Areas.Clubs.Services;

public class ClubQueryService : IClubQueryService
{
    private readonly ApplicationDbContext _context;

    public ClubQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClubIndexModel> ClubIndexQuery(int userProfileId)
    {
        // Returns all Clubs where the User has an existing Membership.

        var clubs = await _context.Clubs
            .Where(c => c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .Select(c => new ClubDTO
            {
                Id = c.Id,
                Name = c.Name,
                Memberships = c.Memberships.Select(m => new MembershipDTO
                {
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO
                    {
                        Id = m.UserProfile.Id,
                        DisplayName = m.UserProfile.DisplayName
                    }
                }).ToList()
            }).ToListAsync();

        var model = new ClubIndexModel();

        foreach (var club in clubs)
        {
            var userMembership = club.Memberships.Where(m => m.UserProfile.Id == userProfileId).First();

            switch (userMembership.Rank)
            {
                case MembershipRank.Leader:
                    model.ClubsLeader.Add(club);
                    break;
                case MembershipRank.Member:
                    model.ClubsMember.Add(club);
                    break;
                case MembershipRank.Pending:
                    model.ClubsPending.Add(club);
                    break;
                default:
                    break;
            }
        }

        return model;
    }

    public async Task<List<ClubDTO>> ClubSearch(int userProfileId, string searchValue)
    {
        // Returns existing Clubs where the User has no Membership.

        var clubs = await _context.Clubs
            .Where(c => EF.Functions.Like(c.Name, $"%{searchValue}%") && !c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .Select(c => new ClubDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

        return clubs;
    }
}
