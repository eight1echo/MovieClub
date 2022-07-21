using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupQueryService
{
    Task<MeetupHomeModel> MeetupHomeQuery(int userProfileId);
}
