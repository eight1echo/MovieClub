namespace MovieClub.Web.Areas.Meetups.Pages.ClubMeetups
{
    public class PreviousClubMeetupsPage : PageModel
    {
        private readonly IMeetupQueryService _meetupQueries;

        public PreviousClubMeetupsPage(IMeetupQueryService meetupQueries)
        {
            _meetupQueries = meetupQueries;
        }

        public ClubMeetupsModel? ClubMeetups { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            ClubMeetups = await _meetupQueries.PreviousClubMeetups(id);

            return Page();
        }
    }
}
