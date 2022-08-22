namespace MovieClub.Web.Areas.Meetups.Pages.Discussion
{
    public class MeetupDiscussionPage : PageModel
    {
        public async Task<IActionResult> OnGet(int meetupId)
        {
            return Page();
        }
    }
}
