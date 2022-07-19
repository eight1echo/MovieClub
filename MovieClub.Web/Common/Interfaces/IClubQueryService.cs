using MovieClub.Web.Areas.Clubs.Pages.Index;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubQueryService
{
    Task<ClubIndexModel> ClubIndexQuery(int userProfileId);
    Task<List<ClubDTO>> ClubSearch(int userProfileId, string searchValue);
}