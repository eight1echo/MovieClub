namespace MovieClub.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICurrentUserService _currentUser;

    public IndexModel(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<IActionResult> OnGet()
    {
        var userProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

        if (userProfileId == 0)
            return RedirectToPage("/Create/CreateProfilePage", new { area = "Users" });

        return RedirectToPage("/Home/UserhomePage", new { area = "Users" });
    }
}
