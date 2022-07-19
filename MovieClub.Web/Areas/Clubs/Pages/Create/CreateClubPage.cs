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
    public CreateClubModel CreateClubModel { get; set; } = new CreateClubModel();

    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (ModelState.IsValid)
        {
            var userProfileId = await _userQueries.GetProfileIdFromSession(HttpContext, User);
            await _clubCommands.CreateClub(userProfileId, CreateClubModel);

            return RedirectToPage("./Index");
        }

        return Page();
    }
}
