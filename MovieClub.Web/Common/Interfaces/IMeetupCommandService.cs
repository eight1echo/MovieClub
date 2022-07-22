using MovieClub.Web.Areas.Meetups.Pages.Create;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupCommandService
{
    Task<int> CreateMeetup(CreateMeetupModel model, int movieId);
}
