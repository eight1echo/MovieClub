namespace MovieClub.Web.Areas.Users;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserMembershipsModel?> GetUserMemberships(int userProfileId)
    {
        var memberships = await _context.UserProfiles.Where(up => up.Id == userProfileId).Select(up => new UserMembershipsModel
        {
            UserProfileId = up.Id,
            UserDisplayName = up.DisplayName,

            Leaderships = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Leader)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        Club = new ClubDTO
                        {
                            Id = m.Club.Id,
                            Name = m.Club.Name
                        }
                    }).ToList(),

            Memberships = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Member)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        Club = new ClubDTO
                        {
                            Id = m.Club.Id,
                            Name = m.Club.Name
                        }
                    }).ToList(),

            Pending = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Pending)
                    .Select(m => new MembershipDTO
                    {
                        Id = m.Id,
                        Rank = m.Rank,
                        Club = new ClubDTO
                        {
                            Id = m.Club.Id,
                            Name = m.Club.Name
                        }
                    }).ToList(),

        }).FirstOrDefaultAsync();

        return memberships;
    }

    public async Task<UserAttendanceModel?> GetUserFutureAttendance(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new UserAttendanceModel
            {
                NextAttendance = up.Attendance
                    .Where(a => a.Meetup.Date > DateTime.Now)
                    .OrderBy(a => a.Meetup.Date)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,

                        Meetup = new MeetupDTO
                        {
                            Id = a.Meetup.Id,
                            Club = new ClubDTO
                            {
                                Id = a.Meetup.Club.Id,
                                Name = a.Meetup.Club.Name
                            },

                            Date = a.Meetup.Date,
                            Location = a.Meetup.Location,

                            Host = a.Meetup.Attendance
                                .Where(a => a.Status == AttendanceStatus.Hosting)
                                .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                                .FirstOrDefault(),

                            Movie = new MovieDTO
                            {
                                Cast = a.Meetup.Movie.Cast,
                                Director = a.Meetup.Movie.Director,
                                Genres = a.Meetup.Movie.Genres,
                                PosterURL = a.Meetup.Movie.PosterURL,
                                ReleaseDate = a.Meetup.Movie.ReleaseDate,
                                Runtime = a.Meetup.Movie.Runtime,
                                Screenwriter = a.Meetup.Movie.Screenwriter,
                                Synopsis = a.Meetup.Movie.Synopsis,
                                Tagline = a.Meetup.Movie.Tagline,
                                Title = a.Meetup.Movie.Title
                            },

                        }
                    }).FirstOrDefault() ?? new AttendanceDTO(),

                FutureAttendance = up.Attendance
                    .Where(a => a.Meetup.Date > DateTime.Now)
                    .OrderBy(a => a.Meetup.Date)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,

                        Meetup = new MeetupDTO
                        {
                            Id = a.Meetup.Id,
                            Club = new ClubDTO { Id = a.Meetup.Club.Id, Name = a.Meetup.Club.Name },
                            Date = a.Meetup.Date,

                            Host = a.Meetup.Attendance
                                .Where(a => a.Status == AttendanceStatus.Hosting)
                                .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                                .First(),

                            Location = a.Meetup.Location,
                            Movie = new MovieDTO { Title = a.Meetup.Movie.Title }
                        },
                    // Skip 1 to remove NextAttendance
                    }).Skip(1).ToList()

            }).FirstOrDefaultAsync();

        if (model is null) throw new ResourceNotFoundException();

        return model;
    }

    public async Task<UserAttendanceModel?> GetUserPastAttendance(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new UserAttendanceModel
            {
                PastAttendance = up.Attendance
                    .Where(a => a.Meetup.Date < DateTime.Now)
                    .OrderBy(a => a.Meetup.Date)
                    .Select(a => new AttendanceDTO
                    {
                        Id = a.Id,
                        Status = a.Status,

                        Meetup = new MeetupDTO
                        {
                            Id = a.Meetup.Id,
                            Club = new ClubDTO { Id = a.Meetup.Club.Id, Name = a.Meetup.Club.Name },
                            Date = a.Meetup.Date,

                            Host = a.Meetup.Attendance
                                .Where(a => a.Status == AttendanceStatus.Hosting)
                                .Select(a => new UserProfileDTO { Id = a.UserProfile.Id, DisplayName = a.UserProfile.DisplayName })
                                .First(),

                            Location = a.Meetup.Location,
                            Movie = new MovieDTO { Title = a.Meetup.Movie.Title }
                        },
                    }).ToList()

            }).FirstOrDefaultAsync();

        return model;
    }
}