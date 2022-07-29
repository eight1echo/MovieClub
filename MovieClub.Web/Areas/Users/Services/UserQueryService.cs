using MovieClub.Web.Areas.Users.Pages.Home;

namespace MovieClub.Web.Areas.Users.Services;

public class UserQueryService : IUserQueryService
{
    private readonly ApplicationDbContext _context;

    public UserQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDTO?> GetUserProfile(string userAccountId)
    {
        var profile = await _context.UserProfiles
            .Where(up => up.UserAccountId == userAccountId)
            .Select(up => new UserProfileDTO { Id = up.Id, DisplayName = up.DisplayName })
            .FirstOrDefaultAsync();

        return profile;
    }

    public async Task<HomePageModel> HomePageQuery(int userProfileId)
    {
        var model = await _context.UserProfiles
            .Where(up => up.Id == userProfileId)
            .Select(up => new HomePageModel
            {
                ClubsLeader = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Leader)
                    .Select(m => new ClubDTO 
                    {
                        Id = m.Club.Id,
                        Name = m.Club.Name,
                        UserMembership = m.Club.Memberships
                            .Where(m => m.UserProfile.Id == userProfileId)
                            .Select(m => new MembershipDTO
                            {
                                Id = m.Id,
                                Rank = m.Rank,
                                UserProfile = new UserProfileDTO { Id = m.UserProfile.Id }
                            }).First(),
                    }).ToList(),

                ClubsMember = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Member)
                    .Select(m => new ClubDTO
                    {
                        Id = m.Club.Id,
                        Name = m.Club.Name,
                        UserMembership = m.Club.Memberships
                            .Where(m => m.UserProfile.Id == userProfileId)
                            .Select(m => new MembershipDTO
                            {
                                Id = m.Id,
                                Rank = m.Rank,
                                UserProfile = new UserProfileDTO { Id = m.UserProfile.Id }
                            }).First(),
                    }).ToList(),

                ClubsPending = up.Memberships
                    .Where(m => m.Rank == MembershipRank.Pending)
                    .Select(m => new ClubDTO
                    {
                        Id = m.Club.Id,
                        Name = m.Club.Name,
                        UserMembership = m.Club.Memberships
                            .Where(m => m.UserProfile.Id == userProfileId)
                            .Select(m => new MembershipDTO
                            {
                                Id = m.Id,
                                Rank = m.Rank,
                                UserProfile = new UserProfileDTO { Id = m.UserProfile.Id }
                            }).First(),
                    }).ToList(),

                UpcomingMeetups = up.Attendance
                    .Where(a => a.UserProfileId == userProfileId)
                    .OrderBy(a => a.Meetup.Date)
                    .Select(a => new MeetupDTO
                    {
                        Id = a.Meetup.Id,
                        Club = new ClubDTO { Id = a.Meetup.Club.Id, Name = a.Meetup.Club.Name },
                        Date = a.Meetup.Date,
                        Movie = new MovieDTO { Title = a.Meetup.Movie.Title },
                        UserAttendance = a.Meetup.Attendance
                            .Where(a => a.UserProfileId == userProfileId)
                            .Select(a => new AttendanceDTO
                            {
                                Id = a.Id,
                                Status = a.Status
                            }).FirstOrDefault() ?? new AttendanceDTO()
                    }).ToList(),

                UserProfile = new UserProfileDTO
                {
                    DisplayName = up.DisplayName
                }

            }).FirstAsync();

        return model;
    }
}