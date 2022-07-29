using MovieClub.Web.Areas.Clubs.Pages.Meetups;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupQueryService
{
    Task<ClubMeetupsModel?> PastClubMeetups(int clubId);
    Task<ClubMeetupsModel?> UpcomingClubMeetups(int clubId);
}
