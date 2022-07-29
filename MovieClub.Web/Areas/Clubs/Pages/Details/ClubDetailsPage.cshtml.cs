namespace MovieClub.Web.Areas.Clubs.Pages.Details
{
    public class ClubDetailsPage : PageModel
    {
        private readonly IAttendanceCommandService _attendanceCommands;
        private readonly IAttendanceQueryService _attendanceQueries;
        private readonly IClubQueryService _clubQueries;
        private readonly IClubCommandService _clubCommands;
        private readonly IMembershipCommandService _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public ClubDetailsPage(IClubQueryService clubQueries,
            ICurrentUserService userService,
            IMembershipCommandService membershipCommands,
            IClubCommandService clubCommmands,
            IAttendanceQueryService attendanceQueries,
            IAttendanceCommandService attendanceCommands)
        {
            _clubQueries = clubQueries;
            _currentUser = userService;
            _membershipCommands = membershipCommands;
            _clubCommands = clubCommmands;
            _attendanceQueries = attendanceQueries;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public ClubDetailsModel PageData { get; set; } = null!;

        [BindProperty]
        public int MembershipUserId { get; set; }

        public List<SelectListItem>? StatusSelectList { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubQueries.ClubDetails(userProfileId, id);
                StatusSelectList = _attendanceQueries.StatusSelect();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteClub()
        {
            if (PageData.UserMembership.Rank == MembershipRank.Leader)
            {
                await _clubCommands.DeleteClub(PageData.ClubId);
                return RedirectToPage("/Home/UserHomePage", new { area = "Users" });
            }
                
            return RedirectToPage("/AccessDenied");
        }

        public async Task<IActionResult> OnPostDeleteMembership()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            if (PageData.UserMembership.Rank == MembershipRank.Leader || PageData.UserMembership.UserProfile.Id == userProfileId)
            {
                await _membershipCommands.DeleteMembership(PageData.UserMembership.Id);
                return await OnGet(PageData.ClubId);
            }

            return RedirectToPage("/AccessDenied");
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            await _attendanceCommands.UpdateStatus(PageData.NextMeetup.UserAttendance.Id, PageData.NextMeetup.UserAttendance.Status);

            return await OnGet(PageData.ClubId);
        }

        public async Task<IActionResult> OnPostApplyToJoin()
        {
            var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
            await _membershipCommands.CreatePending(PageData.ClubId, userProfileId);

            return RedirectToPage("/Home/UserHomePage", new { area = "Users" });
        }

        public async Task<IActionResult> OnPostAcceptMembership()
        {
            if (PageData.UserMembership.Rank is not MembershipRank.Leader)
                return RedirectToPage("/AccessDenied");

            await _membershipCommands.AcceptMembership(PageData.ClubId, MembershipUserId);

            return await OnGet(PageData.ClubId);
        }
    }
}
