namespace MovieClub.Web.Areas.Clubs.Pages.Meetups
{
    public class PastMeetupsPage : PageModel
    {
        private readonly IMeetupQueryService _meetupQueries;

        public PastMeetupsPage(IMeetupQueryService meetupQueries)
        {
            _meetupQueries = meetupQueries;
        }

        public ClubMeetupsModel? PageData { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                PageData = await _meetupQueries.UpcomingClubMeetups(id);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
                throw;
            }
        }
    }
}
