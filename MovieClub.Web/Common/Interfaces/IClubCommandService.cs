using MovieClub.Web.Areas.Clubs.Pages.Create;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubCommandService
{
    Task<int> CreateClub(int userProfileId, CreateClubModel model);
}