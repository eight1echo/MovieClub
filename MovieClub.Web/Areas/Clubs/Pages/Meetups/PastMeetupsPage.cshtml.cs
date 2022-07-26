namespace MovieClub.Web.Areas.Clubs.Pages.Meetups
{
    public class PastMeetupsPage : PageModel
    {
        private readonly IMeetupQueryService _meetupQueries;

        public PastMeetupsPage(IMeetupQueryService meetupQueries)
        {
            _meetupQueries = meetupQueries;
        }

        public ClubMeetupsModel? ClubMeetups { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            ClubMeetups = await _meetupQueries.PastClubMeetups(id);

            return Page();
        }
    }
}
