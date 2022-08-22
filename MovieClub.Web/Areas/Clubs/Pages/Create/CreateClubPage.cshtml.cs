namespace MovieClub.Web.Areas.Clubs.Pages.Create;

public class CreateClubPage : PageModel
{
    private readonly IClubCommands _clubCommands;
    private readonly ICurrentUserService _currentUser;

    public CreateClubPage(IClubCommands clubCommands, ICurrentUserService userService)
    {
        _clubCommands = clubCommands;
        _currentUser = userService;
    }

    [BindProperty]
    public int UserProfileId { get; set; }

    [BindProperty]
    public CreateClubModel PageData { get; set; } = new CreateClubModel();

    public async Task<IActionResult> OnGetAsync()
    {
        UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
        return Page();
    }

    public async Task<IActionResult> OnPostCreateClub()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var clubId = await _clubCommands.Create(UserProfileId, PageData.Name);

                return RedirectToPage("/Details/ClubDetailsPage", new { clubId, area = "Clubs" });
            }

            return Page();
        }
        catch (Exception)
        {
            return RedirectToPage("/Error");
        }
    }
}
