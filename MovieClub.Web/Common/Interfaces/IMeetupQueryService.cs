using MovieClub.Web.Areas.Home;
using MovieClub.Web.Areas.Meetups.Pages.ClubMeetups;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupQueryService
{
    Task<MeetupHomeModel> MeetupHomeQuery(int userProfileId);
    Task<ClubMeetupsModel?> PreviousClubMeetups(int clubId);
    Task<ClubMeetupsModel?> UpcomingClubMeetups(int clubId);
}
