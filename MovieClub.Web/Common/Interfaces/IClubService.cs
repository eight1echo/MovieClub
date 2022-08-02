using MovieClub.Web.Areas.Clubs.Pages.Details;
using MovieClub.Web.Areas.Clubs.Pages.Edit;
using MovieClub.Web.Areas.Clubs.Pages.Meetups;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubService
{
    Task<ClubDetailsModel> DetailsPage(int userProfileId, int clubId);
    Task<EditClubModel?> EditPage(int userProfileId, int clubId);
    Task<List<SelectListItem>> GetSelectList(int userProfileId);
    Task<ClubMeetupsModel> MeetupsFuturePage(int userProfileId, int clubId);
    Task<ClubMeetupsModel> MeetupsPastPage(int userProfileId, int clubId);
    Task<List<ClubDTO>> SearchPage(int userProfileId, string searchValue);
}