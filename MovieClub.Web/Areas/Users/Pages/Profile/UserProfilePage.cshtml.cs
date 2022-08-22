namespace MovieClub.Web.Areas.Users.Pages.Profile
{
    public class UserProfilePage : PageModel
    {
        public async Task<IActionResult> OnGet(int profileId)
        {
            return Page();
        }
    }
}
