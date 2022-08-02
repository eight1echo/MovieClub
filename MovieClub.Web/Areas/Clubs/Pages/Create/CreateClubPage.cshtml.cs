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
    public CreateClubModel PageData { get; set; } = new CreateClubModel();

    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostCreateClub()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                var clubId = await _clubCommands.Create(userProfileId, PageData.Name!);

                return RedirectToPage("/details/ClubDetailsPage", new { id = clubId, area = "Clubs" });
            }

            return Page();
        }
        catch (Exception)
        {
            return RedirectToPage("/Error");
        }
    }
}
