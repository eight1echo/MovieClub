namespace MovieClub.Web.Areas.Users.Pages.Home
{
    public class UserHomePage : PageModel
    {
        private readonly IAttendanceCommandService _attendanceCommands;
        private readonly IAttendanceQueryService _attendanceQueries;
        private readonly IMembershipCommandService _membershipCommands;
        private readonly IUserQueryService _userQueries;
        private readonly ICurrentUserService _currentUser;

        public UserHomePage(IUserQueryService userQueries, ICurrentUserService currentUser, IAttendanceQueryService attendanceQueries, IAttendanceCommandService attendanceCommands, IMembershipCommandService membershipCommands)
        {
            _userQueries = userQueries;
            _currentUser = currentUser;
            _attendanceQueries = attendanceQueries;
            _attendanceCommands = attendanceCommands;
            _membershipCommands = membershipCommands;
        }

        [BindProperty]
        public int UserMembershipId { get; set; }

        public HomePageModel? PageData { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

            PageData = await _userQueries.HomePageQuery(userProfileId);

            if (PageData is null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteMembership(int userId)
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            if (userId == userProfileId)
            {
                await _membershipCommands.DeleteMembership(UserMembershipId);
                return await OnGet();
            }

            return RedirectToPage("/AccessDenied");
        }
    }
}
