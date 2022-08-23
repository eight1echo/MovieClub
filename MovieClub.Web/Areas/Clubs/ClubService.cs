namespace MovieClub.Web.Areas.Clubs;

public class ClubService : IClubService
{
    private readonly ApplicationDbContext _context;

    public ClubService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClubMeetupsModel?> GetClubFutureMeetups(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                UserProfileId = userProfileId,

                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name
                },
                
                FutureMeetups = c.Meetups
                    .Where(m => m.Date > DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => new MeetupDTO 
                    {
                        Id = m.Id,

                        UserAttendance = m.Attendance
                            .Where(a => a.UserProfileId == userProfileId)
                            .Select(a => new AttendanceDTO 
                            { 
                                Id = a.Id,
                                Status = a.Status
                            }).FirstOrDefault(),

                        Date = m.Date,
                        Location = m.Location,

                        Host = m.Attendance
                            .Where(a => a.Status == AttendanceStatus.Hosting)
                            .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                            .FirstOrDefault(),

                        Movie = new MovieDTO
                        {
                            Cast = m.Movie.Cast,
                            Director = m.Movie.Director,
                            Genres = m.Movie.Genres,
                            IMDBPath = m.Movie.IMDbPath,
                            LetterboxPath = m.Movie.LetterboxPath,
                            PosterURL = m.Movie.PosterURL,
                            ReleaseDate = m.Movie.ReleaseDate,
                            Runtime = m.Movie.Runtime,
                            Screenwriter = m.Movie.Screenwriter,
                            Synopsis = m.Movie.Synopsis,
                            Tagline = m.Movie.Tagline,
                            Title = m.Movie.Title
                        }

                    }).ToList()

            }).FirstOrDefaultAsync();

        if (model is not null)
        {
            if (model.FutureMeetups.Any())
            {
                model.NextMeetup = model.FutureMeetups.First();
                model.FutureMeetups.Remove(model.NextMeetup);
            }            
        }
       
        return model;
    }

    public async Task<ClubMeetupsModel?> GetClubPastMeetups(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                UserProfileId = userProfileId,

                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name
                },

                FutureMeetups = c.Meetups
                    .Where(m => m.Date < DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => new MeetupDTO
                    {
                        Id = m.Id,

                        UserAttendance = m.Attendance
                            .Where(a => a.UserProfileId == userProfileId)
                            .Select(a => new AttendanceDTO
                            {
                                Id = a.Id,
                                Status = a.Status
                            }).FirstOrDefault(),

                        Date = m.Date,
                        Location = m.Location,

                        Host = m.Attendance
                                    .Where(a => a.Status == AttendanceStatus.Hosting)
                                    .Select(a => new UserProfileDTO { DisplayName = a.UserProfile.DisplayName })
                                    .FirstOrDefault(),

                        Movie = new MovieDTO
                        {
                            Cast = m.Movie.Cast,
                            Director = m.Movie.Director,
                            Genres = m.Movie.Genres,
                            PosterURL = m.Movie.PosterURL,
                            ReleaseDate = m.Movie.ReleaseDate,
                            Runtime = m.Movie.Runtime,
                            Screenwriter = m.Movie.Screenwriter,
                            Synopsis = m.Movie.Synopsis,
                            Tagline = m.Movie.Tagline,
                            Title = m.Movie.Title
                        },
                    }).ToList()

            }).FirstOrDefaultAsync();

        return model;
    }

    public async Task<ClubMembershipsModel?> GetClubMemberships(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMembershipsModel
            {
                ClubId = c.Id,
                ClubName = c.Name,

                UserMembership = c.Memberships
                    .Where(m => m.UserProfileId == userProfileId)
                    .Select(m => new MembershipDTO
                    {
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId }
                    }).First(),

                Leader = c.Memberships
                    .Where(m => m.Rank == MembershipRank.Leader)
                    .Select(m => new MembershipDTO
                    {
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                    }).First(),

                Memberships = c.Memberships
                .Where(m => m.Rank == MembershipRank.Member)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }

                    }).ToList(),

                Pending = c.Memberships
                .Where(m => m.Rank == MembershipRank.Pending)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }

                    }).ToList(),

            }).FirstOrDefaultAsync();

        return model;
    }

    public async Task<List<SelectListItem>> GetSelectList(int userProfileId)
    {
        var clubSelect = await _context.Clubs
            .Where(c => c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .OrderBy(c => c.Memberships.First(m => m.UserProfileId == userProfileId).Rank)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name}"
            }).ToListAsync();

        return clubSelect;
    }

    public async Task<List<ClubDTO>> SearchPage(int userProfileId, string searchValue)
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
                Memberships = c.Memberships
                    .Where(m => m.Rank != MembershipRank.Pending)
                    .Select(m => new MembershipDTO
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