using Microsoft.AspNetCore.Authorization;

namespace MovieClub.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICurrentUserService _currentUser;

    public IndexModel(ILogger<IndexModel> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> OnGet()
    {
        var userprofileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

        return RedirectToPage("/Profile/UserProfilePage", new { id = userprofileId, area = "Users" });
    }
}
