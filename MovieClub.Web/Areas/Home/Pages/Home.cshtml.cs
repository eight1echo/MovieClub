namespace MovieClub.Web.Areas.Home
{
    public class HomePage : PageModel
    {
        private readonly IClubQueryService _clubQueries;
        private readonly IMeetupQueryService _meetupQueries;
        private readonly IMembershipCommandService _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public HomePage(IClubQueryService clubQueries, 
            ICurrentUserService currentUser, 
            IMembershipCommandService membershipCommands, 
            IMeetupQueryService meetupQueries)
        {
            _clubQueries = clubQueries;
            _currentUser = currentUser;
            _membershipCommands = membershipCommands;
            _meetupQueries = meetupQueries;
        }

        public ClubHomeModel? Clubs { get; set; }
        public MeetupHomeModel? Meetups { get; set; }

        public List<ClubDTO>? SearchedClubs { get; set; }

        [BindProperty]
        public int ClubId { get; set; }

        [BindProperty]
        [Required]
        public string? SearchValue { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

            Clubs = await _clubQueries.ClubHomeQuery(userProfileId);
            Meetups = await _meetupQueries.MeetupHomeQuery(userProfileId);

            return Page();
        }

        public async Task<IActionResult> OnPostCancel()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.CancelMembership(ClubId, userProfileId);

            return await OnGet();
        }

        public async Task<IActionResult> OnPostJoin()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.CreatePendingMembership(ClubId, userProfileId);

            return await OnGet();
        }

        public async Task<IActionResult> OnPostSearch()
        {
            if (ModelState.IsValid)
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                SearchedClubs = await _clubQueries.ClubSearch(userProfileId, SearchValue!);
                return Page();
            }

            return await OnGet();
        }
    }
}
