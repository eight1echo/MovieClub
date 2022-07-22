namespace MovieClub.Web.Areas.Clubs.Pages.Details
{
    public class ClubDetailsPage : PageModel
    {
        private IClubQueryService _clubQueries;
        private readonly ICurrentUserService _userService;

        public ClubDetailsPage(IClubQueryService clubQueries, ICurrentUserService userService)
        {
            _clubQueries = clubQueries;
            _userService = userService;
        }

        public ClubDetailsModel? ClubDetails { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var userProfileId = await _userService.GetProfileIdFromSession(HttpContext, User);

            ClubDetails = await _clubQueries.ClubDetails(userProfileId, id);

            return Page();
        }
    }
}
