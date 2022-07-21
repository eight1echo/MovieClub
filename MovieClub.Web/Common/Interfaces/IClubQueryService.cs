using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubQueryService
{
    Task<ClubHomeModel> ClubHomeQuery(int userProfileId);
    Task<List<ClubDTO>> ClubSearch(int userProfileId, string searchValue);
}