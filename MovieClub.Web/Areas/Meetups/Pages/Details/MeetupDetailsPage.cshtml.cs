namespace MovieClub.Web.Areas.Meetups.Pages.Details
{
    public class MeetupDetailsPage : PageModel
    {
        public async Task<IActionResult> OnGet(int meetupId)
        {
            return Page();
        }
    }
}
