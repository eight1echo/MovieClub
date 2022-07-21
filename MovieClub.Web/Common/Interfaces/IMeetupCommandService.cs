using MovieClub.Web.Areas.Meetups.Pages.Create;

namespace MovieClub.Web.Common.Interfaces;

public interface IMeetupCommandService
{
    Task CreateMeetup(CreateMeetupModel model);
}
