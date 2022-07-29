namespace MovieClub.Web.Areas.Clubs.Pages.Search
{
    public class SearchClubsPage : PageModel
    {
        private readonly IClubQueryService _clubQueries;
        private readonly IMembershipCommandService _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public SearchClubsPage(IClubQueryService clubQueries,
            ICurrentUserService currentUser,
            IMembershipCommandService membershipCommands)
        {
            _clubQueries = clubQueries;
            _currentUser = currentUser;
            _membershipCommands = membershipCommands;
        }

        [BindProperty]
        public int ClubId { get; set; }

        public List<ClubDTO>? SearchedClubs { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostSearch()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

            SearchedClubs = await _clubQueries.ClubSearch(userProfileId, Request.Form["searchvalue"]);
            return Page();
        }

        public async Task<IActionResult> OnPostJoin()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.CreatePending(ClubId, userProfileId);

            return RedirectToPage("/Home/UserHomePage", new { area = "Users" });
        }
    }
}
