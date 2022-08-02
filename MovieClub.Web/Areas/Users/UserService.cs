using MovieClub.Web.Areas.Users.Pages.Edit;
using MovieClub.Web.Areas.Users.Pages.Meetups;
using MovieClub.Web.Areas.Users.Pages.Profile;

namespace MovieClub.Web.Areas.Users;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditUserProfileModel?> EditPage(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new EditUserProfileModel
            {
                NewDisplayName = up.DisplayName,

                UserProfile = new UserProfileDTO
                {
                    Id = up.Id,
                    DisplayName = up.DisplayName
                }
            }).FirstOrDefaultAsync();

        return model;
    }

    public async Task<UserProfileModel?> ProfilePage(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new UserProfileModel
            {
                UserProfile = new UserProfileDTO
                {
                    Id = up.Id,
                    DisplayName = up.DisplayName
                },

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
                                .Select(a => new UserProfileDTO { DisplayName = a.UserProfile.DisplayName })
                                .FirstOrDefault(),

                            Movie = new MovieDTO
                            {
                                Genres = a.Meetup.Movie.Genres,
                                PosterURL = a.Meetup.Movie.PosterURL,
                                ReleaseDate = a.Meetup.Movie.ReleaseDate,
                                Synopsis = a.Meetup.Movie.Synopsis,
                                Tagline = a.Meetup.Movie.Tagline,
                                Title = a.Meetup.Movie.Title
                            }
                        }
                    }).FirstOrDefault()

            }).FirstOrDefaultAsync() ?? new UserProfileModel();

        return model;
    }

    public async Task<UserMeetupsModel?> MeetupsPastPage(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new UserMeetupsModel
            {
                UserProfile = new UserProfileDTO
                {
                    Id = up.Id,
                    DisplayName = up.DisplayName
                },

                Attendance = up.Attendance
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
                        }
                    }).ToList()

            }).FirstOrDefaultAsync() ?? new UserMeetupsModel();

        return model;
    }

    public async Task<UserMeetupsModel?> MeetupsFuturePage(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new UserMeetupsModel
            {
                UserProfile = new UserProfileDTO
                {
                    Id = up.Id,
                    DisplayName = up.DisplayName
                },

                Attendance = up.Attendance
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
                    }).ToList()

            }).FirstOrDefaultAsync() ?? new UserMeetupsModel();

        return model;
    }
}