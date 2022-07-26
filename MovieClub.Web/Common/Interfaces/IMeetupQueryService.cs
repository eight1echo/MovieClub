using MovieClub.Web.Areas.Clubs.Pages.Meetups;
using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupQueryService
{
    Task<MeetupHomeModel> MeetupHomeQuery(int userProfileId);
    Task<ClubMeetupsModel?> PastClubMeetups(int clubId);
    Task<ClubMeetupsModel?> UpcomingClubMeetups(int clubId);
}
