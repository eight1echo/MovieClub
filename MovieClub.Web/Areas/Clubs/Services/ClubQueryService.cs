using MovieClub.Web.Areas.Clubs.Pages.Details;
using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Areas.Clubs.Services;

public class ClubQueryService : IClubQueryService
{
    private readonly ApplicationDbContext _context;

    public ClubQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClubDetailsModel?> ClubDetails(int userProfileId, int clubId)
    {
        var club = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubDetailsModel
            {
                ClubId = c.Id,
                ClubName = c.Name,

                ClubLeader = c.Memberships
                    .Where(m => m.Rank == MembershipRank.Leader)
                    .Select(m => new MembershipDTO
                    {
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                    }).First(),

                ClubMembers = c.Memberships
                    .Where(m => m.Rank == MembershipRank.Member)
                    .Select(m => new MembershipDTO
                    {
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                    }).ToList(),

                NextMeetup = c.Meetups
                    .Where(m => m.Date > DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => new MeetupDTO
                    {
                        Date = m.Date,
                        Host = m.Attendance
                            .SingleOrDefault(a => a.Status == AttendanceStatus.Hosting)!.UserProfile.DisplayName,
                        Location = m.Location,

                        Movie = new MovieDTO
                        {
                            Genres = m.Movie.Genres,
                            PosterURL = m.Movie.PosterURL,
                            ReleaseDate = m.Movie.ReleaseDate,
                            Synopsis = m.Movie.Synopsis,
                            Tagline = m.Movie.Tagline,
                            Title = m.Movie.Title
                        },

                        UserAttendance = m.Attendance
                            .Where(a => a.UserProfileId == userProfileId)
                            .Select(a => new AttendanceDTO
                            {
                                Id = a.Id,
                                Status = a.Status
                            }).FirstOrDefault()

                    }).FirstOrDefault(),

                PendingMemberships = c.Memberships
                .Where(m => m.Rank == MembershipRank.Pending)
                .Select(m => new MembershipDTO
                {
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                }).ToList(),

                UserMembership = c.Memberships
                .Where(m => m.UserProfileId == userProfileId)
                .Select(m => new MembershipDTO
                {
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                }).FirstOrDefault() ?? new MembershipDTO() { Rank = MembershipRank.Visitor },

            }).FirstOrDefaultAsync();

        if (club is null)
            // Club not found
            return null;

        if (club.UserMembership.Rank is MembershipRank.Visitor)
            // User is not a Member of this Club, return limited data.
            return new ClubDetailsModel() 
            { 
                ClubId = club.ClubId,
                ClubLeader = club.ClubLeader, 
                ClubName = club.ClubName, 
            };

        return club;

    }

    public async Task<ClubHomeModel> ClubHomeQuery(int userProfileId)
    {
        // Returns all Clubs where the User has an existing Membership.
        var clubs = await _context.Clubs
            .Where(c => c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .Select(c => new ClubDTO
            {
                Id = c.Id,
                Name = c.Name,

                UserRank = c.Memberships
                    .Where(m => m.UserProfileId == userProfileId)
                    .Select(m => new MembershipDTO { Rank = m.Rank })
                    .First().Rank,

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

        var model = new ClubHomeModel();

        foreach (var club in clubs)
        {
            switch (club.UserRank)
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

        if (searchValue == string.Empty)
        {
            return new List<ClubDTO>();
        }

        var clubs = await _context.Clubs
            .Where(c => EF.Functions.Like(c.Name, $"%{searchValue}%") && !c.Memberships.Any(m => m.UserProfileId == userProfileId))
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

        return clubs;
    }
}
