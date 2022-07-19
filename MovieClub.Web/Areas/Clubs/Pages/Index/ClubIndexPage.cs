namespace MovieClub.Web.Areas.Clubs.Pages.Index;

public class IndexModel : PageModel
{
    private readonly IClubQueryService _clubQueries;
    private readonly IUserQueryService _userQueries;

    public IndexModel(IClubQueryService clubQueries, IUserQueryService userQueries)
    {
        _clubQueries = clubQueries;
        _userQueries = userQueries;
    }

    public ClubIndexModel? ClubIndex { get; set; }
    public List<ClubDTO>? SearchedClubs { get; set; }

    [BindProperty]
    [Required]
    public string? SearchValue { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var userProfileId = await _userQueries.GetProfileIdFromSession(HttpContext, User);
        ClubIndex = await _clubQueries.ClubIndexQuery(userProfileId);

        return Page();
    }

    public async Task<IActionResult> OnPostSearch()
    {
        if (ModelState.IsValid)
        {
            var userProfileId = await _userQueries.GetProfileIdFromSession(HttpContext, User);
            SearchedClubs = await _clubQueries.ClubSearch(userProfileId, SearchValue!);
            return Page();
        }

        return await OnGet();
    }
}
