using MovieClub.Web.Areas.Meetups.Pages.Details;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupService
{
    Task<MeetupDetailsModel> DetailsPage(int userProfileId, int meetupId);
}