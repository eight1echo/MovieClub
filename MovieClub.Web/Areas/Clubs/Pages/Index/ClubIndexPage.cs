using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Areas.Clubs.Pages.Index;

public class ClubIndexPage : PageModel
{
    private readonly IClubQueryService _clubQueries;
    private readonly IMembershipCommandService _membershipCommands;
    private readonly ICurrentUserService _userService;

    public ClubIndexPage(IClubQueryService clubQueries, ICurrentUserService userService, IMembershipCommandService membershipCommands)
    {
        _clubQueries = clubQueries;
        _userService = userService;
        _membershipCommands = membershipCommands;
    }

    public ClubHomeModel? Clubs { get; set; }
    public List<ClubDTO>? SearchedClubs { get; set; }


    [BindProperty]
    public int ClubId { get; set; }

    [BindProperty]
    public string? SearchValue { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
        Clubs = await _clubQueries.ClubHomeQuery(userProfileId);

        return Page();
    }

    public async Task<IActionResult> OnPostCancel()
    {
        var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
        await _membershipCommands.CancelMembership(ClubId, userProfileId);

        return await OnGet();
    }

    public async Task<IActionResult> OnPostJoin()
    {
        var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
        await _membershipCommands.CreatePendingMembership(ClubId, userProfileId);

        return await OnGet();
    }

    public async Task<IActionResult> OnPostSearch()
    {
        if (ModelState.IsValid)
        {
            var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
            SearchedClubs = await _clubQueries.ClubSearch(userProfileId, SearchValue!);
            return Page();
        }

        return await OnGet();
    }
}
