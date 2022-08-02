using MovieClub.Web.Areas.Users.Pages.Edit;
using MovieClub.Web.Areas.Users.Pages.Meetups;
using MovieClub.Web.Areas.Users.Pages.Profile;

namespace MovieClub.Web.Common.Interfaces;

public interface IUserService
{
    Task<EditUserProfileModel?> EditPage(int userProfileId);
    Task<UserMeetupsModel?> MeetupsFuturePage(int userProfileId);
    Task<UserMeetupsModel?> MeetupsPastPage(int userProfileId);
    Task<UserProfileModel?> ProfilePage(int userProfileId);
}