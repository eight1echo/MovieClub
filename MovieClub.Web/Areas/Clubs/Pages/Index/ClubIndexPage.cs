namespace MovieClub.Web.Areas.Clubs.Pages.Index;

public class IndexModel : PageModel
{
    private readonly IClubQueryService _clubQueries;
    private readonly ICurrentUserService _userService;

    public IndexModel(IClubQueryService clubQueries, ICurrentUserService userService)
    {
        _clubQueries = clubQueries;
        _userService = userService;
    }

    public ClubIndexModel? ClubIndex { get; set; }
    public List<ClubDTO>? SearchedClubs { get; set; }

    [BindProperty]
    [Required]
    public string? SearchValue { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);
        ClubIndex = await _clubQueries.ClubIndexQuery(userProfileId);

        return Page();
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
