namespace MovieClub.Web.Common.Interfaces;

public interface IClubService
{
    Task<ClubMeetupsModel?> GetClubFutureMeetups(int userProfileId, int clubId);
    Task<ClubMembershipsModel?> GetClubMemberships(int userProfileId, int clubId);
    Task<ClubMeetupsModel?> GetClubPastMeetups(int userProfileId, int clubId);
    Task<List<SelectListItem>> GetSelectList(int userProfileId);
    Task<List<ClubDTO>> SearchPage(int userProfileId, string searchValue);
}