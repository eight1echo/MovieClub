namespace MovieClub.Web.Areas.Meetups.Pages.ClubMeetups
{
    public class UpcomingClubMeetupsPage : PageModel
    {
        private readonly IMeetupQueryService _meetupQueries;

        public UpcomingClubMeetupsPage(IMeetupQueryService meetupQueries)
        {
            _meetupQueries = meetupQueries;
        }

        public ClubMeetupsModel? ClubMeetups { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            ClubMeetups = await _meetupQueries.UpcomingClubMeetups(id);

            return Page();
        }
    }
}
