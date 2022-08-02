namespace MovieClub.Web.Areas.Clubs.Pages.Details
{
    public class ClubDetailsPage : PageModel
    {
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IClubService _clubPages;
        private readonly IMembershipCommands _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public ClubDetailsPage(IClubService clubPages,
            ICurrentUserService userService,
            IMembershipCommands membershipCommands,
            IAttendanceCommands attendanceCommands)
        {
            _clubPages = clubPages;
            _currentUser = userService;
            _membershipCommands = membershipCommands;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public ClubDetailsModel PageData { get; set; } = null!;

        [BindProperty]
        public int MembershipUserId { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubPages.DetailsPage(userProfileId, id);

                if (PageData is not null)
                    return Page();

                return RedirectToPage("/NotFound");
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
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                if (PageData.UserMembership.Rank == MembershipRank.Leader || PageData.UserMembership.UserProfile.Id == userProfileId)
                {
                    await _membershipCommands.DeleteMembership(PageData.UserMembership.Id);
                    return await OnGet(PageData.Club.Id);
                }

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
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                if (PageData.NextMeetup!.UserAttendance is not null)
                {
                    if (PageData.UserMembership.UserProfile.Id == userProfileId)
                    {
                        await _attendanceCommands.UpdateStatus(PageData.NextMeetup.UserAttendance.Id, PageData.NextMeetup.UserAttendance.Status);

                        return await OnGet(PageData.Club.Id);
                    }

                    return RedirectToPage("/AccessDenied");
                }

                return RedirectToPage("/NotFound");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostApplyToJoin()
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                await _membershipCommands.CreatePendingMembership(PageData.Club.Id, userProfileId);

                return RedirectToPage("/profile/UserProfilePage", new { area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }            
        }

        public async Task<IActionResult> OnPostAcceptMembership()
        {
            try
            {
                if (PageData.UserMembership.Rank is MembershipRank.Leader)
                {
                    await _membershipCommands.AcceptMembership(PageData.Club.Id, MembershipUserId);
                    return await OnGet(PageData.Club.Id);
                }

                return RedirectToPage("/AccessDenied");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
