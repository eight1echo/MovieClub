namespace MovieClub.Web.Areas.Meetups.Pages.Details
{
    public class MeetupDetailsPage : PageModel
    {
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMeetupService _meetupPages;
        private readonly ICurrentUserService _currentUser;

        public MeetupDetailsPage(IMeetupService meetupPages, ICurrentUserService currentUser, IAttendanceCommands attendanceCommands)
        {
            _meetupPages = meetupPages;
            _currentUser = currentUser;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public MeetupDetailsModel PageData { get; set; } = null!;

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _meetupPages.DetailsPage(userProfileId, id);

                if (PageData.UserAttendance is null)
                {
                    return RedirectToPage("/AccessDenied");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            await _attendanceCommands.UpdateStatus(PageData.UserAttendance.Id, PageData.UserAttendance.Status);

            return await OnGet(PageData.Meetup.Id);
        }
    }
}
