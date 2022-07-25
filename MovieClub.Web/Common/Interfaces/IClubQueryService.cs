using MovieClub.Web.Areas.Clubs.Pages.Details;
using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubQueryService
{
    Task<ClubDetailsModel?> ClubDetails(int userProfileId, int clubId);
    Task<ClubHomeModel> ClubHomeQuery(int userProfileId);
    Task<List<ClubDTO>> ClubSearch(int userProfileId, string searchValue);
    Task<List<SelectListItem>> GetClubSelectList(int userProfileId);
}