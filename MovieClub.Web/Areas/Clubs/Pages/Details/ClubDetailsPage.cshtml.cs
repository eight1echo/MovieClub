namespace MovieClub.Web.Areas.Clubs.Pages.Details
{
    public class ClubDetailsPage : PageModel
    {
        private readonly IClubQueryService _clubQueries;
        private readonly IClubCommandService _clubCommands;
        private readonly IMembershipCommandService _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public ClubDetailsPage(IClubQueryService clubQueries, 
            ICurrentUserService userService, 
            IMembershipCommandService membershipCommands, 
            IClubCommandService clubCommmands)
        {
            _clubQueries = clubQueries;
            _currentUser = userService;
            _membershipCommands = membershipCommands;
            _clubCommands = clubCommmands;
        }

        [BindProperty]
        public ClubDetailsModel? ClubDetails { get; set; }

        [BindProperty]
        public int MembershipUserId { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

            ClubDetails = await _clubQueries.ClubDetails(userProfileId, id);

            if (ClubDetails is null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateMembership()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.CreatePending(ClubDetails.ClubId, userProfileId);

            return RedirectToPage("/home", new { area = "Home" });
        }

        public async Task<IActionResult> OnPostCancelMembership()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.Cancel(ClubDetails.ClubId, userProfileId);

            return RedirectToPage("/home", new { area = "Home" });
        }

        public async Task<IActionResult> OnPostAcceptMembership()
        {
            if (ClubDetails is null)
                return RedirectToPage("/Error");

            if (ClubDetails.UserMembership.Rank is not MembershipRank.Leader)
                return RedirectToPage("/AccessDenied");

            await _membershipCommands.Accept(ClubDetails.ClubId, MembershipUserId);

            return await OnGet(ClubDetails.ClubId);
        }

        public async Task<IActionResult> OnPostDenyMembership()
        {
            if (ClubDetails is null)
                return RedirectToPage("/Error");

            if (ClubDetails.UserMembership.Rank is not MembershipRank.Leader)
                return RedirectToPage("/AccessDenied");

            await _membershipCommands.Cancel(ClubDetails.ClubId, MembershipUserId);

            return await OnGet(ClubDetails.ClubId);
        }

        public async Task<IActionResult> OnPostDeleteClub()
        {
            if (ClubDetails is null)
                return RedirectToPage("/Error");

            if (ClubDetails.UserMembership.Rank is not MembershipRank.Leader)
                return RedirectToPage("/AccessDenied");

            await _clubCommands.Delete(ClubDetails.ClubId);

            return RedirectToPage("/Home", new { area = "Home" });
        }
    }
}
