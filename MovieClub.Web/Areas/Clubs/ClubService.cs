using MovieClub.Web.Areas.Clubs.Pages.Details;
using MovieClub.Web.Areas.Clubs.Pages.Edit;
using MovieClub.Web.Areas.Clubs.Pages.Meetups;

namespace MovieClub.Web.Areas.Clubs;

public class ClubService : IClubService
{
    private readonly ApplicationDbContext _context;

    public ClubService(ApplicationDbContext context)
    {
        _context = context;
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

    public async Task<ClubDetailsModel> DetailsPage(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubDetailsModel
            {
                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClubLeader = c.Memberships
                        .Where(m => m.Rank == MembershipRank.Leader)
                        .Select(m => new UserProfileDTO
                        {
                            Id = m.UserProfileId,
                            DisplayName = m.UserProfile.DisplayName
                        }).FirstOrDefault()
                },

                ClubMembers = c.Memberships
                    .Where(m => m.Rank == MembershipRank.Member)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                    }).ToList() ?? new List<MembershipDTO>(),

                NextMeetup = c.Meetups
                    .Where(m => m.Date > DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => new MeetupDTO
                    {
                        Id = m.Id,
                        Date = m.Date,

                        Host = m.Attendance
                            .Where(a => a.Status == AttendanceStatus.Hosting)
                            .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                            .FirstOrDefault(),

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
                            }).FirstOrDefault() ?? new AttendanceDTO()

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
                    Id = m.Id,
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                }).FirstOrDefault() ?? new MembershipDTO() { Rank = MembershipRank.Visitor },

            }).FirstOrDefaultAsync();

        if (model is null)
            throw new Exception();

        if (model.UserMembership.Rank is MembershipRank.Visitor)
            // User is not a Member of this Club, return limited data.
            return new ClubDetailsModel()
            {
                Club = new ClubDTO
                {
                    Id = model.Club.Id,
                    ClubLeader = model.Club.ClubLeader,
                    Name = model.Club.Name

                }
            };

        return model;
    }

    public async Task<EditClubModel?> EditPage(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new EditClubModel
            {
                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name
                },

                NewName = c.Name,

                UserMembership = c.Memberships
                .Where(m => m.UserProfileId == userProfileId)
                .Select(m => new MembershipDTO
                {
                    Id = m.Id,
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO { Id = m.UserProfileId, DisplayName = m.UserProfile.DisplayName }
                }).FirstOrDefault() ?? new MembershipDTO() { Rank = MembershipRank.Visitor },

            }).FirstOrDefaultAsync();

        return model;
    }

    public async Task<ClubMeetupsModel> MeetupsFuturePage(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name
                },

                Attendance = c.Meetups
                    .Where(m => m.Date > DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => m.Attendance.Where(a => a.UserProfileId == userProfileId).First())
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,

                        Meetup = new MeetupDTO
                        {
                            Id = a.Meetup.Id,
                            Date = a.Meetup.Date,

                            Host = a.Meetup.Attendance
                                .Where(a => a.Status == AttendanceStatus.Hosting)
                                .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                                .First(),

                            Location = a.Meetup.Location,
                            Movie = new MovieDTO { Title = a.Meetup.Movie.Title }
                        },
                    }).ToList()

            }).FirstOrDefaultAsync() ?? new ClubMeetupsModel();

        return model;
    }

    public async Task<ClubMeetupsModel> MeetupsPastPage(int userProfileId, int clubId)
    {
        var model = await _context.Clubs
            .Where(c => c.Id == clubId)
            .Select(c => new ClubMeetupsModel
            {
                Club = new ClubDTO
                {
                    Id = c.Id,
                    Name = c.Name
                },

                Attendance = c.Meetups
                    .Where(m => m.Date < DateTime.Now)
                    .OrderBy(m => m.Date)
                    .Select(m => m.Attendance.Where(a => a.UserProfileId == userProfileId).First())
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,

                        Meetup = new MeetupDTO
                        {
                            Id = a.Meetup.Id,
                            Date = a.Meetup.Date,

                            Host = a.Meetup.Attendance
                                .Where(a => a.Status == AttendanceStatus.Hosting)
                                .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                                .First(),

                            Location = a.Meetup.Location,
                            Movie = new MovieDTO { Title = a.Meetup.Movie.Title }
                        },
                    }).ToList()

            }).FirstOrDefaultAsync() ?? new ClubMeetupsModel();

        return model;
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