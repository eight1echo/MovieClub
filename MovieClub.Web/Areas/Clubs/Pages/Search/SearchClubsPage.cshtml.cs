namespace MovieClub.Web.Areas.Clubs.Pages.Search
{
    public class SearchClubsPage : PageModel
    {
        private readonly IClubService _clubPages;
        private readonly IMembershipCommands _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public SearchClubsPage(IClubService clubQueries,
            ICurrentUserService currentUser,
            IMembershipCommands membershipCommands)
        {
            _clubPages = clubQueries;
            _currentUser = currentUser;
            _membershipCommands = membershipCommands;
        }

        [BindProperty]
        public int ClubId { get; set; }

        public List<ClubDTO> SearchedClubs { get; set; } = new List<ClubDTO>();

        public async Task<IActionResult> OnGet(string value)
        {
            try
            {
                var userProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
                SearchedClubs = await _clubPages.SearchPage(userProfileId, value);
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public IActionResult OnPostSearch(string value)
        {
            try
            {
                return RedirectToPage("/Search/SearchClubsPage", new { value, area = "Clubs" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostJoin()
        {
            try
            {
                var userProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
                await _membershipCommands.CreatePendingMembership(ClubId, userProfileId);

                return RedirectToPage("/Home/UserHomePage", new { area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }            
        }
    }
}
