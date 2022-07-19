namespace MovieClub.Web.Areas.Clubs.Pages.Create;

public class CreateClubPage : PageModel
{
    private readonly IClubCommandService _clubCommands;
    private readonly IUserQueryService _userQueries;

    public CreateClubPage(IClubCommandService clubCommandService, IUserQueryService userQueries)
    {
        _clubCommands = clubCommandService;
        _userQueries = userQueries;
    }

    [BindProperty]
    public CreateClubModel? CreateClubModel { get; set; }

    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var userProfile = await _userQueries.GetCurrentUserProfile(User);
            await _clubCommands.CreateClub(userProfile.Id, CreateClubModel);

            return RedirectToPage("./Index");
        }

        return Page();
    }
}
