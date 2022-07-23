using MovieClub.Web.Areas.Clubs.Pages.Create;

namespace MovieClub.Web.Common.Interfaces;

public interface IClubCommandService
{
    Task<int> Create(int userProfileId, CreateClubModel model);
    Task Delete(int clubId);
}