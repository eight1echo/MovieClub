namespace MovieClub.Web.Areas.Users.Pages.Home
{
    public class UserHomePage : PageModel
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMembershipCommands _membershipCommands;
        private readonly IUserService _userService;

        public UserHomePage(
            ICurrentUserService currentUser, 
            IUserService userService, 
            IAttendanceCommands attendanceCommands,
            IMembershipCommands membershipCommands)
        {
            _currentUser = currentUser;
            _userService = userService;
            _attendanceCommands = attendanceCommands;
            _membershipCommands = membershipCommands;
        }

        [BindProperty]
        public int UserProfileId { get; set; }

        [BindProperty]
        public UserAttendanceModel? UserAttendance { get; set; }

        [BindProperty]
        public UserMembershipsModel? UserMemberships { get; set; }

        public bool _pastAttendance;
        public bool _upcomingAttendance;

        public async Task<IActionResult> OnGet()
        {
            try
            {
                UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

                UserAttendance = await _userService.GetUserFutureAttendance(UserProfileId);
                UserMemberships = await _userService.GetUserMemberships(UserProfileId);

                _upcomingAttendance = true;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnGetPastMeetups()
        {
            try
            {
                UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

                UserAttendance = await _userService.GetUserPastAttendance(UserProfileId);
                UserMemberships = await _userService.GetUserMemberships(UserProfileId);

                _pastAttendance = true;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostDeleteMembership()
        {
            try
            {
                var userProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
                if (UserMemberships is not null && UserMemberships.UserProfileId == userProfileId)
                {
                    await _membershipCommands.DeleteMembership(UserMemberships.MembershipId);
                    return RedirectToPage("/Home/UserhomePage", new { area = "Users" });
                }
                else
                    return RedirectToPage("/AccessDenied");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            try
            {
                if (UserAttendance is not null)
                {
                    await _attendanceCommands.UpdateStatus(UserAttendance.AttendanceId, UserAttendance.AttendanceStatus);

                    return RedirectToPage("/Home/UserhomePage", new { area = "Users" });
                }

                return RedirectToPage("/Error");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
