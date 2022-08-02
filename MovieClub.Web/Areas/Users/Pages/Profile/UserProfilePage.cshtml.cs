namespace MovieClub.Web.Areas.Users.Pages.Profile
{
    public class UserProfilePage : PageModel
    {
        private readonly IUserService _userPages;
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMembershipCommands _membershipCommands;
        private readonly ICurrentUserService _currentUser;

        public UserProfilePage(ICurrentUserService currentUser, IMembershipCommands membershipCommands, IAttendanceCommands attendanceCommands, IUserService userPages)
        {
            _currentUser = currentUser;
            _membershipCommands = membershipCommands;
            _attendanceCommands = attendanceCommands;
            _userPages = userPages;
        }

        [BindProperty]
        public int MembershipId { get; set; }

        [BindProperty]
        public UserProfileModel? PageData { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

                if (userProfileId != 0)
                {
                    if (userProfileId == id)
                    {
                        PageData = await _userPages.ProfilePage(id);

                        if (PageData is not null)
                            return Page();

                        return RedirectToPage("/NotFound");
                    }

                    return RedirectToPage("/Profile/VisitorProfilePage", new { area = "Users" });
                }

                return RedirectToPage("/Create/CreateProfilePage", new { area = "Users" });
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
                if (PageData is not null)
                {
                    var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                    if (PageData.UserProfile.Id == userProfileId)
                    {
                        await _membershipCommands.DeleteMembership(MembershipId);
                        return await OnGet(PageData.UserProfile.Id);
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

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            try
            {
                if (PageData is not null)
                {
                    if (PageData.NextAttendance is not null)
                        await _attendanceCommands.UpdateStatus(PageData.NextAttendance.Id, PageData.NextAttendance.Status);

                    return RedirectToPage("/Profile/UserProfilePage", new { id = PageData.UserProfile.Id, area = "Users" });
                }

                return RedirectToPage("/NotFound");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
