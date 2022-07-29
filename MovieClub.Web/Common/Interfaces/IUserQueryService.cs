using MovieClub.Web.Areas.Users.Pages.Home;

namespace MovieClub.Web.Common.Interfaces;

public interface IUserQueryService
{
    Task<UserProfileDTO?> GetUserProfile(string userAccountId);
    Task<HomePageModel> HomePageQuery(int userProfileId);
}