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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                SearchedClubs = await _clubPages.SearchPage(userProfileId, Request.Form["searchvalue"]);
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostJoin()
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                await _membershipCommands.CreatePendingMembership(ClubId, userProfileId);

                return RedirectToPage("/profile/UserProfilePage", new { id = userProfileId, area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }            
        }
    }
}
