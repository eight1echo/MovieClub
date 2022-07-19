namespace MovieClub.Web.Areas.Clubs.Pages.Create;

public class CreateClubPage : PageModel
{
    private readonly IClubCommandService _clubCommands;
    private readonly ICurrentUserService _userService;

    public CreateClubPage(IClubCommandService clubCommandService, ICurrentUserService userService)
    {
        _clubCommands = clubCommandService;
        _userService = userService;
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
            var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
            await _clubCommands.CreateClub(userProfileId, CreateClubModel);

            return RedirectToPage("./Index");
        }

        return Page();
    }
}
